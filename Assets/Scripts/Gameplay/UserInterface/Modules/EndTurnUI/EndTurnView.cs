using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UserInterface.Modules.EndTurnUI
{
    public class EndTurnView : MonoBehaviour
    {
        [SerializeField] private Button endTurnButton;
    
        public event Action ButtonClicked;

        public void ActivateButton(bool isActive)
        {
            endTurnButton.interactable = isActive;
        }

        private void OnEnable()
        {
            endTurnButton.onClick.AddListener(HandleButtonClicked);
        }

        private void OnDisable()
        {
            endTurnButton.onClick.RemoveListener(HandleButtonClicked);
        }

        private void HandleButtonClicked()
        {
            ButtonClicked?.Invoke();
        }
    }
}