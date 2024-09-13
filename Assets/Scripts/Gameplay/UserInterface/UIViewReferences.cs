using Gameplay.UserInterface.Modules.EndTurnUI;
using Gameplay.UserInterface.Modules.Health;
using Gameplay.UserInterface.Modules.MoneyUI;
using Gameplay.UserInterface.Modules.PlayerHandUI;
using UnityEngine;

namespace Gameplay.UserInterface
{
    public class UIViewReferences : MonoBehaviour
    {
        [SerializeField] private PlayerMoneyView playerMoneyView;
        public PlayerMoneyView PlayerMoneyView => playerMoneyView;
    
        [SerializeField] private PlayerHealthView playerHealthView;
        public PlayerHealthView PlayerHealthView => playerHealthView;

        [SerializeField] private EndTurnView endTurnView;
        public EndTurnView EndTurnView => endTurnView;

        [SerializeField] private PlayerHandView playerHandView;
        public PlayerHandView PlayerHandView => playerHandView;
    }
}
