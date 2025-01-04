using System.Threading;
using Cysharp.Threading.Tasks;

namespace Gameplay.Enemies
{
    public class StunEnemyEffect : IEnemyEffect
    {
        private CancellationTokenSource cts;
        public void Apply(EnemyPresenter enemyPresenter)
        {
            cts?.Cancel();
            cts = new CancellationTokenSource();
            Stun(enemyPresenter, cts.Token).Forget();
        }

        private async UniTask Stun(EnemyPresenter enemyPresenter, CancellationToken cancellationToken)
        {
            if (enemyPresenter is IMovable movable)
            {
                cancellationToken.Register(() => movable.ContinueMovement());
                movable.PauseMovement();
                enemyPresenter.View.SetAnimatorSpeed(0);
                await UniTask.WaitForSeconds(1.5f, cancellationToken: cts.Token);
                movable.ContinueMovement();
                enemyPresenter.View.SetAnimatorSpeed(1);
            }
        }

        public void Cancel()
        {
            cts?.Cancel();
        }
    }
}