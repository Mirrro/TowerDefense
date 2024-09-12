using System.Threading;
using Cysharp.Threading.Tasks;

public class GameplayCard
{
    public int Cost;
    
    private CancellationTokenSource cts;
    private IGameplayTask gameplayTask;
    
    public GameplayCard(IGameplayTask gameplayTask)
    {
        this.gameplayTask = gameplayTask;
    }

    public async UniTask Execute()
    {
        cts?.Cancel();
        cts = new CancellationTokenSource();
        await gameplayTask.Execute(cts.Token);
    }

    public void Cancel()
    {
        cts?.Cancel();
    } 
}