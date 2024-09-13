using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Player;
using Zenject;

namespace Gameplay.GameplayTasks
{
    public class HelloWorldGameplayTask : IGameplayTask
    {
        [Inject] private PlayerHealth playerHealth;
    
        public UniTask Execute(CancellationToken cancellationToken)
        {
            playerHealth.AddHealth(1);
            return UniTask.CompletedTask;
        }
    
        public class Factory : PlaceholderFactory<HelloWorldGameplayTask>
        {
        
        }
    }
}