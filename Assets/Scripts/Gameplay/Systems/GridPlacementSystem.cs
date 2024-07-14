using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GridPlacementSystem
{
    private readonly GridManager gridManager;
    private readonly GridInteraction gridInteraction;
    private readonly EnemyManager enemyManager;
    private PathFinding pathFinding = new ();
    private ConvertService convertService = new ();

    private readonly TaskQueue placementQueue = new ();

    public GridPlacementSystem(GridManager gridManager, GridInteraction gridInteraction, EnemyManager enemyManager)
    {
        this.gridManager = gridManager;
        this.gridInteraction = gridInteraction;
        this.enemyManager = enemyManager;
    }

    public void PlaceElement(IGridElement gridElement, Action callback)
    {
        placementQueue.EnqueueTask(() => PlaceElementTask(gridElement), callback);
    }

    private async UniTask PlaceElementTask(IGridElement gridElement)
    {
        var completionSource = new UniTaskCompletionSource();
        
        void HandleGridSelected(Vector2Int position)
        {
            GridNode selectedGridNode = gridManager.Grid.GridNodes[position.x, position.y];
            if (!selectedGridNode.IsSolid && EnsurePath(position))
            {
                selectedGridNode.AddGirdElement(gridElement);
                gridInteraction.OnGridCellSelected -= HandleGridSelected;
                completionSource.TrySetResult();
            }
        }

        gridInteraction.OnGridCellSelected += HandleGridSelected;
        await completionSource.Task;
        gridInteraction.OnGridCellSelected -= HandleGridSelected;
    }

    private bool EnsurePath(Vector2Int position)
    {
        var convertedGrid = convertService.ConvertGridNodes(gridManager.Grid.GridNodes);
        convertedGrid[position.x, position.y].IsWalkable = false;
        var path = pathFinding.GetPath(convertedGrid, enemyManager.StartPos, enemyManager.EndPos);
        return path.Any();
    }
}