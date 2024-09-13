using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.GameplayCards
{
    public class GameplayCardDeck
    {
        private List<GameplayCard> gameplayCards = new ();

        public void AddGameplayCard(GameplayCard gameplayCard)
        {
            gameplayCards.Add(gameplayCard);
        }

        public bool TryDrawRandom(out GameplayCard card)
        {
            if (gameplayCards.Any())
            {
                card = gameplayCards.ElementAt(Random.Range(0, gameplayCards.Count));
                gameplayCards.Remove(card);
                return true;
            }

            card = null;
            return false;
        }
    }
}