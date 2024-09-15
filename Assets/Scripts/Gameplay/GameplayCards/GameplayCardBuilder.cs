using Gameplay.GameplayTasks;
using Gameplay.UserInterface.Modules.PlayerHandUI;
using Zenject;

namespace Gameplay.GameplayCards
{
    public class GameplayCardBuilder
    {
        [Inject] private BuildTowerATask.Factory buildTowerATaskFactory;
        [Inject] private BuildTowerBTask.Factory buildTowerBTaskFactory;
        [Inject] private BuildTowerCTask.Factory buildTowerCTaskFactory;
        [Inject] private HelloWorldGameplayTask.Factory hWFactory;

        public GameplayCard CreateShockclaw()
        {
            return new GameplayCard(buildTowerATaskFactory.Create(), new GameplayCardData()
            {
                CardCost = 200,
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
        
        public GameplayCard CreateFrostbite()
        {
            return new GameplayCard(buildTowerCTaskFactory.Create(), new GameplayCardData()
            {
                CardCost = 350,
                CardName = "Frostbite",
                Description = "Is it just me, or is anyone else feeling cold?."
            });
        }
    }
}