using System.Collections.Generic;
using Gameplay.GameplayCards;
using UnityEngine.Events;

namespace Gameplay.UserInterface.Modules.PlayerHandUI
{
    public class PlayerHandPresenter
    {
        private readonly PlayerHandView playerHandView;
        private readonly PlayerHandModel playerHandModel;
    
        public UnityEvent<GameplayCard> CardPlayed = new ();

        public PlayerHandPresenter(PlayerHandView playerHandView, PlayerHandModel playerHandModel)
        {
            this.playerHandView = playerHandView;
            this.playerHandModel = playerHandModel;
        }

        public void Activate()
        {
            playerHandView.CardPlayed.AddListener(OnCardPlayed);
            playerHandView.OnActivate();
        }

        public void Deactivate()
        {
            playerHandView.CardPlayed.RemoveListener(OnCardPlayed);
            playerHandView.OnDeactivate();
        }
    
        public void AddCard(GameplayCard card)
        {
            playerHandModel.GameplayCardsInHand.Add(card);
            playerHandView.AddCard(card);
        }

        public void RemoveCard(GameplayCard gameplayCard)
        {
            if (playerHandModel.GameplayCardsInHand.Contains(gameplayCard))
            {
                playerHandModel.GameplayCardsInHand.Remove(gameplayCard);
                playerHandView.RemoveCard(gameplayCard);
            }
        }

        private void OnCardPlayed(GameplayCard card)
        {
            CardPlayed?.Invoke(card);
            RemoveCard(card);
        }
    }

    public class PlayerHandModel
    {
        public List<GameplayCard> GameplayCardsInHand = new();
    }
}