using System.Collections.Generic;
using System.Collections.Generic;
using DG.Tweening;
using Gameplay.GameplayCards;
using Gameplay.GameplayCards;
using UnityEngine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Events;

namespace Gameplay.UserInterface.Modules.PlayerHandUI
{
    public class PlayerHandView : MonoBehaviour
    {
        [SerializeField] private GameplayCardUI gameplayCardUI;
        [SerializeField] private RectTransform rectTransform;
        public UnityEvent<GameplayCard> CardPlayed = new ();
        private Dictionary<GameplayCard, GameplayCardUI> gameplayCardUis = new ();

        private Tween activateTween;

        public void OnActivate()
        {
            activateTween?.Kill(true);
            activateTween = rectTransform.DOLocalMove(Vector3.down * (Screen.height / 2f), 1f);
        }
        
        public void OnDeactivate()
        {
            activateTween?.Kill(true);
            activateTween = rectTransform.DOLocalMove(Vector3.down * (Screen.height / 2f) - new Vector3(0,rectTransform.rect.height / 2f, 0), 1f);
        }
    
        public void AddCard(GameplayCard card)
        {
            var cardUI = Instantiate(gameplayCardUI, transform);
            cardUI.Initialize(card.Data);
            gameplayCardUis.Add(card, cardUI);
            cardUI.OnClicked.AddListener(() => CardPlayed.Invoke(card));
        }

        public void RemoveCard(GameplayCard card)
        {
            if (gameplayCardUis.TryGetValue(card, out var value))
            {
                gameplayCardUis.Remove(card);
                value.OnClicked.RemoveAllListeners();
                Destroy(value.gameObject);
            }
        }
    }

    public class GameplayCardData
    {
        public string CardName;
        public string Description;
        public int CardCost;
    }
}