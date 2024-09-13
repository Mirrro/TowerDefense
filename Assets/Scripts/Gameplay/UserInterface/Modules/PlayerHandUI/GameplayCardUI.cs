using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UserInterface.Modules.PlayerHandUI
{
    public class GameplayCardUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameTextfield;
        [SerializeField] private TMP_Text descriptionTextfield;
        [SerializeField] private TMP_Text costTextfield;
        [SerializeField] private Button button;
        public Button.ButtonClickedEvent OnClicked => button.onClick;

        public void Initialize(GameplayCardData data)
        {
            nameTextfield.text = data.CardName;
            descriptionTextfield.text = data.Description;
            costTextfield.text = data.CardCost.ToString();
        }
    }
}
