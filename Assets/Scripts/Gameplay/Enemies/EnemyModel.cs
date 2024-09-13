using UnityEngine;

namespace Gameplay.Enemies
{
    public class EnemyModel
    {
        public float Health;
        public Vector3 position;
        public Vector2Int gridTargetPosition;
        public float MovementSpeed = 1;
    

        public EnemyModel(float health)
        {
            Health = health;
        }
    }
}
