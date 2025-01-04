using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Enemies
{
    public interface IEnemyView
    {
        Transform TargetPoint { get; }
        Transform Transform { get; }
        UnityEvent MouseDown { get; }
        void SetPosition(Vector3 position);
        void SetRotation(Quaternion rotation);
        void Flash();
        void Spawn();
        void SetWalk(bool isWalking);
        void SetAnimatorSpeed(float speed);
        void SetDead(bool isDead);
        void SetHealthBarFill(float percentage);
    }
}