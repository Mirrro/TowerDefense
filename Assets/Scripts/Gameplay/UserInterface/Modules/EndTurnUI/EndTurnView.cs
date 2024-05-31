using System;
using UnityEngine;
using UnityEngine.UI;

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