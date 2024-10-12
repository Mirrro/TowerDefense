using UnityEngine;

namespace Gameplay.Enemies
{
    public interface ITargetable
    {
        public Transform GetTarget();

        public bool CanBeTargeted { get; }
    }
}