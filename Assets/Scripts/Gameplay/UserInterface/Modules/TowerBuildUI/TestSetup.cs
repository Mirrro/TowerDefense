using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TestSetup : MonoBehaviour
{
    [SerializeField] private Button executeBtn;
    [SerializeField] private Button cancelBtn;
    [Inject] private BuildBasicTowerTask.Factory bBFactory;
    [Inject] private HelloWorldGameplayTask.Factory hWFactory;
    private GameplayCardDeck deck = new GameplayCardDeck();
    private GameplayCard activeCard;
    

    private void Awake()
    {
        deck.AddGameplayCard(new GameplayCard(bBFactory.Create()));
        deck.AddGameplayCard(new GameplayCard(hWFactory.Create()));
        executeBtn.onClick.AddListener(OnExecute);
        cancelBtn.onClick.AddListener(OnCancel);
    }

    private void OnExecute()
    {
        activeCard?.Cancel();
        activeCard = deck.DrawRandom();
        activeCard.Execute();
    }

    private void OnCancel()
    {
        activeCard.Cancel();
    }
}
