using Gameplay.Player;
using Gameplay.UserInterface.Modules.EndTurnUI;
using Gameplay.UserInterface.Modules.Health;
using Gameplay.UserInterface.Modules.MoneyUI;
using Gameplay.UserInterface.Modules.PlayerHandUI;
using Zenject;

namespace Gameplay.UserInterface
{
    public class UIManager : IInitializable
    {
        [Inject] private readonly PlayerBank playerBank;
        [Inject] private readonly PlayerHealth playerHealth;
    
        private readonly UIViewReferences uiViewReferences;
        private PlayerMoneyPresenter playerMoneyPresenter;
        private EndTurnPresenter endTurnPresenter;
        private PlayerHealthPresenter playerHealthPresenter;
        private PlayerHandPresenter playerHandPresenter;

        public EndTurnPresenter EndTurnPresenter => endTurnPresenter;
        public PlayerHandPresenter PlayerHandPresenter => playerHandPresenter;

        public UIManager(UIViewReferences uiViewReferences)
        {
            this.uiViewReferences = uiViewReferences;
        }

        public void Initialize()
        {
            playerMoneyPresenter = new PlayerMoneyPresenter(uiViewReferences.PlayerMoneyView, new PlayerMoneyModel(), playerBank);
            playerHealthPresenter =
                new PlayerHealthPresenter(uiViewReferences.PlayerHealthView, new PlayerHealthModel(), playerHealth);
            endTurnPresenter = new EndTurnPresenter(new EndTurnModel(), uiViewReferences.EndTurnView);
            playerHandPresenter = new PlayerHandPresenter(uiViewReferences.PlayerHandView, new PlayerHandModel());
        
            playerHealthPresenter.Initialize();
            playerMoneyPresenter.Initialize();
            endTurnPresenter.Initialize();
        }
    }
}