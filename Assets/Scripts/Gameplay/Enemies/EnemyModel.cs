using UnityEngine;

namespace Gameplay.Enemies
{
    public class EnemyModel
    {
        public int Health;
        public Vector3 position;
        public Vector2Int gridTargetPosition;
        public float MovementSpeed;
    

        public EnemyModel(int health, float movementSpeed)
        {
            Health = health;
            MovementSpeed = movementSpeed;
        }
    }
}
