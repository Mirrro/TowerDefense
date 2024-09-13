using Gameplay.GameplayTasks;
using Gameplay.UserInterface.Modules.PlayerHandUI;
using Zenject;

namespace Gameplay.GameplayCards
{
    public class GameplayCardBuilder
    {
        [Inject] private BuildTowerATask.Factory buildTowerATaskFactory;
        [Inject] private BuildTowerBTask.Factory buildTowerBTaskFactory;
        [Inject] private HelloWorldGameplayTask.Factory hWFactory;

        public GameplayCard CreateShockclaw()
        {
            return new GameplayCard(buildTowerATaskFactory.Create(), new GameplayCardData()
            {
                CardCost = 100,
                CardName = "Shockclaw",
                Description = "Create a Tower of type A. Until yet no one knows what that even means."
            });
        }
    
        public GameplayCard CreateVitalWaters()
        {
            return new GameplayCard(hWFactory.Create(), new GameplayCardData()
            {
                CardCost = 0,
                CardName = "Vital Waters",
                Description = "Gain one hit point. Aaand... It's for free!"
            });
        }
    
        public GameplayCard CreateCinderstrike()
        {
            return new GameplayCard(buildTowerBTaskFactory.Create(), new GameplayCardData()
            {
                CardCost = 750,
                CardName = "Cinderstrike",
                Description = "Create a Tower of type B. I think that might be good."
            });
        }
    }
}