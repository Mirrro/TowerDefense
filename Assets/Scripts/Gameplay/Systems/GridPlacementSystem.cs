using System;
using System.Collections.Concurrent;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GridPlacementSystem
{
    private readonly GridManager gridManager;
    private readonly GridInteraction gridInteraction;
    
    private readonly TaskQueue placementQueue = new ();

    public GridPlacementSystem(GridManager gridManager, GridInteraction gridInteraction)
    {
        this.gridManager = gridManager;
        this.gridInteraction = gridInteraction;
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
            if (!selectedGridNode.IsSolid)
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
}

public class TaskQueue
{
    private ConcurrentQueue<(Func<UniTask>, Action)> taskQueue = new ();
    private bool isProcessing = false;

    public void EnqueueTask(Func<UniTask> task, Action callback = null)
    {
        taskQueue.Enqueue((task, callback));
        if (!isProcessing)
        {
            ProcessNextTask().Forget();
        }
    }

    private async UniTaskVoid ProcessNextTask()
    {
        isProcessing = true;
        while (taskQueue.TryDequeue(out var taskWithCallback))
        {
            var (task, callback) = taskWithCallback;
            await task();
            callback?.Invoke();
        }
        isProcessing = false;
    }
}