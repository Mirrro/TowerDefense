using UnityEngine;

namespace Gameplay.Enemies
{
    public interface IEnemyPresenter
    {
        public Vector3 GetPosition();
        public Transform GetTransform();
        public void Initialize();
        public void Move(Vector2Int target);

        public void StealGold();
        public void ReceiveDamage(int amount);

        public void Freeze(float duration);
    }
}