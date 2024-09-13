using System.Threading;
using Cysharp.Threading.Tasks;

namespace Gameplay.GameplayTasks
{
    public interface IGameplayTask
    {
        public UniTask Execute(CancellationToken cancellationToken);
    }
}