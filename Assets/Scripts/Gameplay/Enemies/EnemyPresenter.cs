using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Grid;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Gameplay.Enemies
{
    public class EnemyPresenter : IEnemyPresenter
    {
        [Inject] private GridManager gridManager;
    
        public UnityEvent Died = new ();
        public UnityEvent ReachedGoal = new ();
    
        private EnemyModel model;
        private EnemyView view;

        private CancellationTokenSource cancellationTokenSource;
        
        public Vector3 GetPosition()
        {
            return model.position;
        }

        public Transform GetTransform()
        {
            return view.transform;
        }

        public EnemyPresenter(EnemyView view, EnemyModel model, GridManager gridManager)
        {
            this.view = view;
            this.model = model;
            this.gridManager = gridManager;
        }
    
        public void Initialize()
        {
            view.PositionChanged.AddListener(HandlePositionChange);
            view.DestinationReached.AddListener(HandleDestinationReached);
        }

        public void Move(Vector2Int target)
        {
            model.gridTargetPosition = target;
            var path = gridManager.GetPath(gridManager.WorldToGridPosition(model.position), model.gridTargetPosition);
            view.MoveTo(path[1]);
        }

        public void StopMove()
        {
            view.StopMove();
        }
    
        public void SetPosition(Vector3 position)
        {
            model.position = position;
            view.UpdatePosition(position);
        }

        public void ReceiveDamage(int amount)
        {
            model.Health -= amount;
            view.ReceiveDamage();
        
            if (model.Health <= 0)
            {
                view.Die();
                Died?.Invoke();
                view.DestinationReached.RemoveListener(HandleDestinationReached);
            }
        }

        public void Freeze(float duration)
        {
            cancellationTokenSource?.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
            SetFreeze(duration, cancellationTokenSource.Token).Forget();
        }

        private async UniTask SetFreeze(float duration, CancellationToken cancellationToken)
        {
            StopMove();
            view.SetFreeze(true);
            await UniTask.WaitForSeconds(duration, cancellationToken: cancellationToken);
            view.SetFreeze(false);
            Move(model.gridTargetPosition);
        }

        public void StealGold()
        {
            view.Die();
        }

        private void HandlePositionChange(Vector3 position)
        {
            model.position = position;
        }

        private void HandleDestinationReached()
        {
            if (gridManager.WorldToGridPosition(model.position) == model.gridTargetPosition)
            {
                ReachedGoal?.Invoke();
            }
            else
            {
                var path = gridManager.GetPath(gridManager.WorldToGridPosition(model.position), model.gridTargetPosition);
                view.MoveTo(path[1]);
            }
        }
    
        public class Factory : PlaceholderFactory<EnemyView, EnemyModel, EnemyPresenter>
        {
        
        }
    }
}