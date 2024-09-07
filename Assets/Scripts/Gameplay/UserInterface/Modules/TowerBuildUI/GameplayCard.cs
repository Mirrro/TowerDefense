using Cysharp.Threading.Tasks;

public class GameplayCard
{
    private GameplayTask gameplayTask;

    public GameplayCard(GameplayTask gameplayTask)
    {
        this.gameplayTask = gameplayTask;
    }

    public void Execute()
    {
        gameplayTask.Execute().Forget();
    }

    public void Cancel()
    {
        gameplayTask?.Cancel();
    } 
}