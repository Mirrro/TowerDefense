using System;
using Gameplay.GameplayCards;
using Gameplay.Grid;
using Gameplay.Player;
using Gameplay.UserInterface;
using Zenject;

namespace Gameplay.GameplayLoop.GameplayStateMachine.GameplayStates
{
    public class InitializationState : IGameplayState
    {
        [Inject] private LevelGenerator.LevelGenerator levelGenerator;
        [Inject] private PlayerBank playerBank;
        [Inject] private PlayerHealth playerHealth;
        [Inject] private GridManager gridManager;
        [Inject] private GameplayCardDeck gameplayCardDeck;
        [Inject] private GameplayCardBuilder gameplayCardBuilder;

        public event Action StateComplete;
        public GameplayStateStatus GameplayStateStatus { get; set; }
        public void Activate()
        {
            levelGenerator.PopulateGrid(gridManager.Grid);
            playerBank.AddMoney(2000);
            playerHealth.AddHealth(10);
            PopulateDeck();
        
            StateComplete?.Invoke();
        }

        private void PopulateDeck()
        {
            for (int i = 0; i < 20; i++)
            {
                gameplayCardDeck.AddGameplayCard(gameplayCardBuilder.CreateShockclaw());
            }
            for (int i = 0; i < 20; i++)
            {
                gameplayCardDeck.AddGameplayCard(gameplayCardBuilder.CreateFrostbite());
            }
            for (int i = 0; i < 5; i++)
            {
                gameplayCardDeck.AddGameplayCard(gameplayCardBuilder.CreateCinderstrike());
            }
            for (int i = 0; i < 5; i++)
            {
                gameplayCardDeck.AddGameplayCard(gameplayCardBuilder.CreateVitalWaters());
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
        }
    }
}