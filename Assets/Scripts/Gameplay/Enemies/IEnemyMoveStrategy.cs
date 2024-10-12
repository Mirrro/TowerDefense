using UnityEngine;

namespace Gameplay.Enemies
{
    public interface IEnemyMoveStrategy
    {
        public void Update(ref Vector3 position, Vector2Int target, float deltaTime);
    }
}