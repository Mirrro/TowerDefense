using UnityEngine;

namespace Gameplay.Enemies
{
    public class EnemyModel
    {
        public int MaxHealth;
        public int Health;
        public Vector3 Position;
        public Vector2Int GridTargetPosition;
        public float MovementSpeed;
        public int MovementPauseCounter;
        
        public EnemyModel(int health, float movementSpeed)
        {
            MaxHealth = health;
            Health = health;
            MovementSpeed = movementSpeed;
        }
    }
}
