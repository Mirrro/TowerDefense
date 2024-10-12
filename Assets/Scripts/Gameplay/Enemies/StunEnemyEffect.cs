using System.Threading;
using Cysharp.Threading.Tasks;

namespace Gameplay.Enemies
{
    public class StunEnemyEffect : IEnemyEffect
    {
        private CancellationTokenSource cts;
        public void Apply(IEnemyPresenter enemyPresenter)
        {
            cts?.Cancel();
            cts = new CancellationTokenSource();
            Stun(enemyPresenter, cts.Token).Forget();
        }

        private async UniTask Stun(IEnemyPresenter enemyPresenter, CancellationToken cancellationToken)
        {
            if (enemyPresenter is IMovable movable)
            {
                cancellationToken.Register(() => movable.ContinueMovement());
                movable.PauseMovement();
                await UniTask.WaitForSeconds(1.5f, cancellationToken: cts.Token);
                movable.ContinueMovement();
            }
        }

        public void Cancel()
        {
            cts?.Cancel();
        }
    }
}