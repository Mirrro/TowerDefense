using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Gameplay.Enemies
{
    public class EnemyPresenter : ITickable, IMovable, IDamageable, ITargetable, IAffectable
    {
        public UnityEvent Died = new ();

        public readonly EnemyModel Model;
        public readonly IEnemyView View;
        
        private readonly IEnemyMoveStrategy enemyMoveStrategy;
        private readonly IEnemyDamageStrategy enemyDamageStrategy;
        
        private List<IEnemyEffect> activeEffects = new ();
        
        public EnemyPresenter(IEnemyView view, EnemyModel model, IEnemyMoveStrategy enemyMoveStrategy, IEnemyDamageStrategy enemyDamageStrategy)
        {
            View = view;
            Model = model;
            this.enemyMoveStrategy = enemyMoveStrategy;
            this.enemyDamageStrategy = enemyDamageStrategy;
            View.MouseDown.AddListener(HandleMouseDown);
        }

        private void HandleMouseDown()
        {
            Debug.Log("You have a click on me :3");
        }
        
        public Transform GetTarget()
        {
            return View.TargetPoint;
        }

        public bool CanBeTargeted => Model.Health > 0;
        public bool IsAlive => Model.Health > 0;

        public void SetPosition(Vector3 position)
        {
            View.Spawn();
            Model.Position = position;
            View.SetPosition(position);
        }

        public void ReceiveDamage(int damage)
        {
            enemyDamageStrategy.ReceiveDamage(ref Model.Health, damage);
            View.Flash();
            View.SetHealthBarFill((float) Model.Health/Model.MaxHealth);
            if (Model.Health <= 0)
            {
                Died?.Invoke();
                View.SetDead(true);
                PauseMovement();
            }
        }
        
        public void SetTarget(Vector2Int target)
        {
            Model.GridTargetPosition = target;
        }

        public void PauseMovement()
        {
            Model.MovementPauseCounter++;
        }

        public void ContinueMovement()
        {
            Model.MovementPauseCounter--;
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
            if (Model.MovementPauseCounter <= 0)
            {
                Vector3 previousPos = Model.Position;
                enemyMoveStrategy.Update(ref Model.Position, ref Model.Rotation, Model.GridTargetPosition, Model.MovementSpeed * Time.deltaTime);
                Vector3 currentPos = Model.Position;
                View.SetPosition(Model.Position);
                View.SetRotation(Model.Rotation);
                View.SetWalk(true);
            }
            else
            {
                View.SetWalk(false);
            }
        }
        
        public void Dispose()
        {
            Died?.RemoveAllListeners();
        }

        public class Factory : PlaceholderFactory<IEnemyView, EnemyModel, IEnemyMoveStrategy, IEnemyDamageStrategy, EnemyPresenter>
        {
        
        }
    }
}