using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

public class HelloWorldGameplayTask : GameplayTask
{
    [Inject] private PlayerHealth playerHealth;
    protected override UniTask OnExecute(CancellationToken cancellationToken)
    {
        playerHealth.AddHealth(1);
        return UniTask.CompletedTask;
    }

    protected override void OnCancel()
    {
        playerHealth.RemoveHealth(1);
    }
    
    public class Factory : PlaceholderFactory<HelloWorldGameplayTask>
    {
        
    }
}