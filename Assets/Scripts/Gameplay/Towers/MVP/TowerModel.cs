using UnityEngine;

namespace Gameplay.Towers.MVP
{
    public class TowerModel
    {
        public Vector3 Position;
        public int Range;
        public int Damage;
        public float ReloadTime;
        public float MaxReloadTime;

        public TowerModel(Vector3 position, int range, int damage, float maxReloadTime)
        {
            Position = position;
            Range = range;
            Damage = damage;
            MaxReloadTime = maxReloadTime;
        }
    }
}