using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.GameplayTasks;
using Gameplay.UserInterface.Modules.PlayerHandUI;

namespace Gameplay.GameplayCards
{
    public class GameplayCard
    {
        public GameplayCardData Data;
    
        private CancellationTokenSource cts;
        private IGameplayTask gameplayTask;
    
        public GameplayCard(IGameplayTask gameplayTask, GameplayCardData data)
        {
            Data = data;
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
}