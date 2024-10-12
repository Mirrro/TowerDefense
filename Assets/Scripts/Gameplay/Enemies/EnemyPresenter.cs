using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Gameplay.Enemies
{
    public class EnemyPresenter : IEnemyPresenter, ITickable, IMovable, IDamageable, ITargetable, IAffectable
    {
        public UnityEvent Died = new ();
        public UnityEvent ReachedGoal = new ();
    
        private readonly EnemyModel model;
        private readonly EnemyView view;
        
        private readonly IEnemyMoveStrategy enemyMoveStrategy;
        private readonly IEnemyDamageStrategy enemyDamageStrategy;
        
        private bool isMovementPaused = false;
        private List<IEnemyEffect> activeEffects = new ();
        
        public EnemyPresenter(EnemyView view, EnemyModel model, IEnemyMoveStrategy enemyMoveStrategy, IEnemyDamageStrategy enemyDamageStrategy)
        {
            this.view = view;
            this.model = model;
            this.enemyMoveStrategy = enemyMoveStrategy;
            this.enemyDamageStrategy = enemyDamageStrategy;
        }
        
        public Vector3 GetPosition()
        {
            return model.position;
        }
        
        public Transform GetTarget()
        {
            return view.transform;
        }

        public bool CanBeTargeted => model.Health > 0;

        public void SetPosition(Vector3 position)
        {
            model.position = position;
            view.SetPosition(position);
        }

        public void ReceiveDamage(int damage)
        {
            enemyDamageStrategy.ReceiveDamage(ref model.Health, damage);
            view.Flash();
            if (model.Health <= 0)
            {
                Died?.Invoke();
                view.Dead();
                PauseMovement();
            }
        }
        
        public void SetTarget(Vector2Int target)
        {
            model.gridTargetPosition = target;
        }

        public void PauseMovement()
        {
            isMovementPaused = true;
        }

        public void ContinueMovement()
        {
            isMovementPaused = false;
        }
        
        public void AddEffect(IEnemyEffect effect)
        {
            effect.Apply(this);
            activeEffects.Add(effect);
        }

        public void RemoveEffect(IEnemyEffect effect)
        {
            if (activeEffects.Contains(effect))
            {
                effect.Cancel();
            }
        }

        public void Tick()
        {
            if (!isMovementPaused)
            {
                Vector3 previousPos = model.position;
                enemyMoveStrategy.Update(ref model.position, model.gridTargetPosition, model.MovementSpeed * Time.deltaTime);
                Vector3 currentPos = model.position;
                view.SetPosition(model.position);
                view.SetRotation(Quaternion.LookRotation((currentPos - previousPos).normalized, view.transform.up));
                view.SetAnimationState("isWalking", true);
            }
        }

        public class Factory : PlaceholderFactory<EnemyView, EnemyModel, IEnemyMoveStrategy, IEnemyDamageStrategy, EnemyPresenter>
        {
        
        }
    }
}