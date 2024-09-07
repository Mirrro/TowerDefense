using System.Threading;
using Cysharp.Threading.Tasks;

public abstract class GameplayTask
{
    private CancellationTokenSource cts;

    public async UniTask Execute()
    {
        cts?.Cancel();
        cts = new CancellationTokenSource();
        var registration = cts.Token.Register(OnCancel);
        await OnExecute(cts.Token);
        await registration.DisposeAsync();
    }
    
    protected virtual async UniTask OnExecute(CancellationToken cancellationToken)
    {
    }

    public void Cancel()
    {
        cts.Cancel();
    }

    protected virtual void OnCancel()
    {
    }
}