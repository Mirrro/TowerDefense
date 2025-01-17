using UnityEngine;

namespace Gameplay.Enemies
{
    public interface IEnemyMoveStrategy
    {
        public void Update(ref Vector3 position, ref Quaternion rotation, Vector2Int target, float deltaTime);
    }
}