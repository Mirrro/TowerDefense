using UnityEngine;

public class UIViewReferences : MonoBehaviour
{
    [SerializeField] private PlayerMoneyView playerMoneyView;
    public PlayerMoneyView PlayerMoneyView => playerMoneyView;
}
