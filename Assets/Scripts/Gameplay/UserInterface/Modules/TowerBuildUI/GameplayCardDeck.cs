using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameplayCardDeck
{
    private List<GameplayCard> gameplayCards = new ();

    public void AddGameplayCard(GameplayCard gameplayCard)
    {
        gameplayCards.Add(gameplayCard);
    }

    public GameplayCard DrawRandom()
    {
        return gameplayCards.ElementAt(Random.Range(0, gameplayCards.Count));
    }
}