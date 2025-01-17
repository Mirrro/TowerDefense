using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Blocks.Water;
using Gameplay.Enemies;
using Gameplay.Grid;
using Gameplay.PathFinding;
using UnityEngine;

namespace Gameplay.Systems
{
    public class GridPlacementSystem
    {
        private readonly GridManager gridManager;
        private readonly GridInteraction gridInteraction;
        private readonly EnemyManager enemyManager;
        private PathFinding.PathFinding pathFinding = new ();
        private ConvertService convertService = new ();

        public GridPlacementSystem(GridManager gridManager, GridInteraction gridInteraction, EnemyManager enemyManager)
        {
            this.gridManager = gridManager;
            this.gridInteraction = gridInteraction;
            this.enemyManager = enemyManager;
        }

        public async UniTask UserPlaceElement(IPlaceable gridElement, CancellationToken cancellationToken)
        {
            var completionSource = new UniTaskCompletionSource();
        
            cancellationToken.Register(() =>
            {
                gridInteraction.OnGridCellHovered -= HandleGridHovered;
                gridInteraction.OnGridCellSelected -= HandleGridSelected;
                completionSource.TrySetCanceled();
            });
            
            void HandleGridHovered(Vector2Int position)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    completionSource.TrySetCanceled();
                    return;
                }

                GridNode hoveredGridNode = gridManager.Grid.GridNodes[position.x, position.y];
                if (!hoveredGridNode.IsSolid && EnsurePath(position))
                {
                    gridElement.HoverGridPosition(new Vector3(position.x, 0, position.y));
                }
            }

            void HandleGridSelected(Vector2Int position)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    completionSource.TrySetCanceled();
                    return;
                }
                
                GridNode selectedGridNode = gridManager.Grid.GridNodes[position.x, position.y];
                if (!selectedGridNode.IsSolid && EnsurePath(position))
                {
                    selectedGridNode.AddGirdElement(gridElement);
                    completionSource.TrySetResult();
                }
            }

            gridInteraction.OnGridCellSelected += HandleGridSelected;
            gridInteraction.OnGridCellHovered += HandleGridHovered;
            await completionSource.Task;
            gridInteraction.OnGridCellSelected -= HandleGridSelected;
            gridInteraction.OnGridCellHovered -= HandleGridHovered;
        }

        /// <summary>
        /// This one can place only on water blocks. ToDo: Can we unify the placement functions and not hardcode the criteria for placement?
        /// </summary>
        /// <param name="gridElement"></param>
        /// <param name="cancellationToken"></param>
        public async UniTask UserPlaceBridge(IPlaceable gridElement, CancellationToken cancellationToken)
        {
            var completionSource = new UniTaskCompletionSource();
        
            cancellationToken.Register(() =>
            {
                gridInteraction.OnGridCellHovered -= HandleGridHovered;
                gridInteraction.OnGridCellSelected -= HandleGridSelected;
                completionSource.TrySetCanceled();
            });
            
            void HandleGridHovered(Vector2Int position)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    completionSource.TrySetCanceled();
                    return;
                }

                GridNode hoveredGridNode = gridManager.Grid.GridNodes[position.x, position.y];
                if (hoveredGridNode.GridElements.Any(x => x is WaterBlockPresenter) && EnsurePath(position))
                {
                    gridElement.HoverGridPosition(new Vector3(position.x, 0, position.y));
                }
            }

            void HandleGridSelected(Vector2Int position)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    completionSource.TrySetCanceled();
                    return;
                }
                
                GridNode selectedGridNode = gridManager.Grid.GridNodes[position.x, position.y];
                if (selectedGridNode.GridElements.Any(x => x is WaterBlockPresenter) && EnsurePath(position))
                {
                    selectedGridNode.AddGirdElement(gridElement);
                    completionSource.TrySetResult();
                }
            }

            gridInteraction.OnGridCellSelected += HandleGridSelected;
            gridInteraction.OnGridCellHovered += HandleGridHovered;
            await completionSource.Task;
            gridInteraction.OnGridCellSelected -= HandleGridSelected;
            gridInteraction.OnGridCellHovered -= HandleGridHovered;
        }

        private bool EnsurePath(Vector2Int position)
        {
            var convertedGrid = convertService.ConvertGridNodes(gridManager.Grid.GridNodes);
            convertedGrid[position.x, position.y].IsWalkable = false;
            return pathFinding.TryGetPath(out var path, convertedGrid, enemyManager.StartPos, enemyManager.EndPos);
        }
    }
}