using Gameplay.Grid;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class EnemyMovementStrategy : IEnemyMoveStrategy
    {
        private readonly GridManager gridManager;

        private Vector3 nextTile;
    
        public EnemyMovementStrategy(GridManager gridManager)
        {
            this.gridManager = gridManager;
        }

        public void Update(ref Vector3 position, Vector2Int target, float deltaTime)
        {
            var direction = nextTile - position;
            position += direction.normalized * deltaTime;
            if (Vector3.Distance(nextTile, position) < .01f)
            {
                TryFindNextPathPosition(ref nextTile, position, target);
            }
        }
    
        private bool TryFindNextPathPosition(ref Vector3 nextTile, Vector3 position, Vector2Int target)
        {
            var path = gridManager.GetPath(gridManager.WorldToGridPosition(position), target);
            if (path.Count <= 1) return false;
            nextTile = path[1];
            return true;
        }
    }
}