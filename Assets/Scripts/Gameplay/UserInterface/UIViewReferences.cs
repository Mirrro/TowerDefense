using UnityEngine;

public class UIViewReferences : MonoBehaviour
{
    [SerializeField] private PlayerMoneyView playerMoneyView;
    public PlayerMoneyView PlayerMoneyView => playerMoneyView;

    [SerializeField] private EndTurnView endTurnView;

    public EndTurnView EndTurnView => endTurnView;
    
    [SerializeField] private BuildMenuView buildMenuView;

    public BuildMenuView BuildMenuView => buildMenuView;
}
