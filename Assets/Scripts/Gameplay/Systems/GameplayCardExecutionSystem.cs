using Cysharp.Threading.Tasks;
using Gameplay.GameplayCards;
using Gameplay.Player;
using Gameplay.UserInterface;
using Zenject;

namespace Gameplay.Systems
{
    public class GameplayCardExecutionSystem : IInitializable
    {
        [Inject] private PlayerBank playerBank;
        [Inject] private UIManager uiManager;

        public void Initialize()
        {
            uiManager.PlayerHandPresenter.CardPlayed.AddListener(x => OnCardPlayed(x).Forget());
        }

        private async UniTask OnCardPlayed(GameplayCard card)
        {
            if (card.Data.CardCost <= playerBank.Coins)
            {
                await card.Execute();
                playerBank.RemoveMoney(card.Data.CardCost);
            }
        }
    }
}