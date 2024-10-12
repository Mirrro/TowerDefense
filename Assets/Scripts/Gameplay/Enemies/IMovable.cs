using UnityEngine;

namespace Gameplay.Enemies
{
    public interface IMovable
    {
        public void SetTarget(Vector2Int target);
        public void PauseMovement();
        public void ContinueMovement();
    }
}