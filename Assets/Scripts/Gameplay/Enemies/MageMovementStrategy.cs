using System.Collections.Generic;
using Gameplay.Grid;
using Gameplay.PathFinding;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class MageMovementStrategy : IEnemyMoveStrategy
    {
        private readonly GridManager gridManager;

        private Vector3 currentTile;
        private Vector3 nextTile;
        private int currentStepIndex;
        private List<Vector3> pathSteps;
        private bool hasSkipped = false;

        public MageMovementStrategy(GridManager gridManager)
        {
            this.gridManager = gridManager;
            pathSteps = new List<Vector3>();
            currentStepIndex = 0;
        }

        public void Update(ref Vector3 position, ref Quaternion rotation, Vector2Int target, float deltaTime)
        {
            if (PathNeedsUpdate(position, target))
            {
                UpdatePath(position, target);
            }

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
                    AdvanceToNextTile();
                }
            }
        }

        private bool PathNeedsUpdate(Vector3 position, Vector2Int target)
        {
            if (pathSteps.Count == 0 || currentStepIndex >= pathSteps.Count ||
                !gridManager.WorldToGridPosition(pathSteps[^1]).Equals(target))
            {
                return true;
            }

            return false;
        }

        private void UpdatePath(Vector3 position, Vector2Int target)
        {
            var path = gridManager.GetPath(gridManager.WorldToGridPosition(position), target);

            if (hasSkipped)
            {
                RestoreWalkability(gridManager.WorldToGridPosition(position));
                path = gridManager.GetPathManipulated(gridManager.WorldToGridPosition(position), target, gridManager.GetGridNodes());
                hasSkipped = false;
            }
            else if (RNGSkip())
            {
                ManipulateWalkability(gridManager.WorldToGridPosition(position));
                path = gridManager.GetPathManipulated(gridManager.WorldToGridPosition(position), target, gridManager.GetGridNodes());
                hasSkipped = true;
            }

            if (path.IsValid && path.Steps.Count > 1)
            {
                pathSteps = path.Steps;
                currentStepIndex = 0;
                currentTile = pathSteps[currentStepIndex];
                nextTile = pathSteps[currentStepIndex + 1];
            }
            else
            {
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

        private void ManipulateWalkability(Vector2Int position)
        {
            var nodes = gridManager.GetGridNodes();

            foreach (var offset in new[]
                     {
                         new Vector2Int(0, 1), new Vector2Int(0, -1),
                         new Vector2Int(1, 0), new Vector2Int(-1, 0)
                     })
            {
                var adjacentPosition = position + offset;
                if (gridManager.IsInBound(adjacentPosition))
                {
                    nodes[adjacentPosition.x, adjacentPosition.y].IsWalkable = true;
                }
            }
        }

        private void RestoreWalkability(Vector2Int position)
        {
            var nodes = gridManager.GetGridNodes();

            foreach (var offset in new[]
                     {
                         new Vector2Int(0, 1), new Vector2Int(0, -1),
                         new Vector2Int(1, 0), new Vector2Int(-1, 0)
                     })
            {
                var adjacentPosition = position + offset;
                if (gridManager.IsInBound(adjacentPosition))
                {
                    nodes[adjacentPosition.x, adjacentPosition.y].IsWalkable = false;
                }
            }
        }

        private bool RNGSkip()
        {
            return Random.Range(0, 100) < 50; // 50% chance to skip
        }
    }
}
