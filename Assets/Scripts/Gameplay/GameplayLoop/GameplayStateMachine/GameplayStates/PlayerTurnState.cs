using System;
using Gameplay.GameplayCards;
using Gameplay.Systems.EnemyPathMVP;
using Gameplay.UserInterface;

namespace Gameplay.GameplayLoop.GameplayStateMachine.GameplayStates
{
    public class PlayerTurnState : IGameplayState
    {
        private readonly UIManager uiManager;
        private readonly EnemyPathPresenter enemyPathPresenter;
        private readonly GameplayCardDeck gameplayCardDeck;
        

        public PlayerTurnState(UIManager uiManager, 
            EnemyPathPresenter enemyPathPresenter,
            GameplayCardDeck gameplayCardDeck)
        {
            this.uiManager = uiManager;
            this.enemyPathPresenter = enemyPathPresenter;
            this.gameplayCardDeck = gameplayCardDeck;
        }

        public event Action StateComplete;
        public GameplayStateStatus GameplayStateStatus { get; set; }

        public void Activate()
        {
            enemyPathPresenter.Activate();
            uiManager.EndTurnPresenter.ActivateButton(true);
            uiManager.EndTurnPresenter.TurnEnded += HandleTurnEnd;
            uiManager.PlayerHandPresenter.Activate();

            for (int i = 0; i < 5; i++)
            {
                GivePlayerCards();
            }
        }

        private void HandleTurnEnd()
        {
            StateComplete?.Invoke();
        }
        
        private void GivePlayerCards()
        {
            if (gameplayCardDeck.TryDrawRandom(out var card))
            {
                uiManager.PlayerHandPresenter.AddCard(card);
            }
        }

        public void OnPause()
        {
        }

        public void OnUnpause()
        {
        }

        public void Update()
        {
        }

        public void Deactivate()
        {
            enemyPathPresenter.Deactivate();
            uiManager.EndTurnPresenter.ActivateButton(false);
            uiManager.EndTurnPresenter.TurnEnded -= HandleTurnEnd;
            uiManager.PlayerHandPresenter.Deactivate();
        }
    }
}