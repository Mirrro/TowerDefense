using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GridPlacementSystem
{
    private readonly GridManager gridManager;
    private readonly GridInteraction gridInteraction;
    private readonly EnemyManager enemyManager;
    private PathFinding pathFinding = new ();
    private ConvertService convertService = new ();

    public GridPlacementSystem(GridManager gridManager, GridInteraction gridInteraction, EnemyManager enemyManager)
    {
        this.gridManager = gridManager;
        this.gridInteraction = gridInteraction;
        this.enemyManager = enemyManager;
    }

    public async UniTask UserPlaceElement(IGridElement gridElement, CancellationToken cancellationToken)
    {
        var completionSource = new UniTaskCompletionSource();
        
        cancellationToken.Register(() =>
        {
            gridInteraction.OnGridCellSelected -= HandleGridSelected;
            completionSource.TrySetCanceled();
        });
        
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