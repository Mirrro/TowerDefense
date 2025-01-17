using System.Collections.Generic;
using Gameplay.Grid;
using UnityEngine;

namespace Gameplay.Enemies
{
   public class EnemyMovementStrategy : IEnemyMoveStrategy
{
    private readonly GridManager gridManager;

    private Vector3 currentTile;
    private Vector3 nextTile;
    private int currentStepIndex;
    private List<Vector3> pathSteps;

    public EnemyMovementStrategy(GridManager gridManager)
    {
        this.gridManager = gridManager;
        pathSteps = new List<Vector3>();
        currentStepIndex = 0;
    }

    public void Update(ref Vector3 position, ref Quaternion rotation, Vector2Int target, float deltaTime)
    {
        // Update the path if the target has changed or path needs recalculation
        if (PathNeedsUpdate(position, target))
        {
            UpdatePath(position, target);
        }

        // Move along the path
        if (currentStepIndex < pathSteps.Count - 1)
        {
            Vector3 direction = nextTile - position;
            float distance = direction.magnitude;

            if (distance > 0.01f)
            {
                position += direction.normalized * deltaTime;
                rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
            }
            else
            {
                // Arrived at the current target tile; advance to the next step
                AdvanceToNextTile();
            }
        }
    }

    private bool PathNeedsUpdate(Vector3 position, Vector2Int target)
    {
        // Check if the path is invalid or the target has changed
        if (pathSteps.Count == 0 || currentStepIndex >= pathSteps.Count ||
            !gridManager.WorldToGridPosition(pathSteps[pathSteps.Count - 1]).Equals(target))
        {
            return true;
        }

        return false;
    }

    private void UpdatePath(Vector3 position, Vector2Int target)
    {
        var path = gridManager.GetPath(gridManager.WorldToGridPosition(position), target);
        if (path.IsValid && path.Steps.Count > 1)
        {
            pathSteps = path.Steps;
            currentStepIndex = 0;
            currentTile = pathSteps[currentStepIndex];
            nextTile = pathSteps[currentStepIndex + 1];
        }
        else
        {
            // If no valid path, clear steps to prevent further movement
            pathSteps.Clear();
        }
    }

    private void AdvanceToNextTile()
    {
        currentStepIndex++;
        if (currentStepIndex < pathSteps.Count - 1)
        {
            currentTile = nextTile;
            nextTile = pathSteps[currentStepIndex + 1];
        }
    }
}

}