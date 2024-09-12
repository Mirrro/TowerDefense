using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TestSetup : MonoBehaviour
{
    [SerializeField] private Button executeBtn;
    [SerializeField] private Button cancelBtn;
    
    [Inject] private BuildTowerATask.Factory buildTowerATaskFactory;
    [Inject] private BuildTowerBTask.Factory buildTowerBTaskFactory;
    [Inject] private HelloWorldGameplayTask.Factory hWFactory;
    [Inject] private PlayerBank playerBank;
    
    private GameplayCardDeck deck = new ();
    private GameplayCard activeCard;
    

    private void Awake()
    {
        deck.AddGameplayCard(new GameplayCard(buildTowerATaskFactory.Create()));
        deck.AddGameplayCard(new GameplayCard(hWFactory.Create()));
        deck.AddGameplayCard(new GameplayCard(buildTowerBTaskFactory.Create()));
        
        executeBtn.onClick.AddListener(() => OnExecute().Forget());
        cancelBtn.onClick.AddListener(OnCancel);
    }

    private async UniTask OnExecute()
    {
        activeCard?.Cancel();
        activeCard = deck.DrawRandom();
        
        if (activeCard.Cost <= playerBank.Coins)
        {
            await activeCard.Execute();
            playerBank.RemoveMoney(activeCard.Cost);
        }
    }

    private void OnCancel()
    {
        activeCard.Cancel();
    }
}
