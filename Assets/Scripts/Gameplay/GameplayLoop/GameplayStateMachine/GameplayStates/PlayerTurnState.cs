using System;

public class PlayerTurnState : IGameplayState
{
    private readonly BuildingSystem buildingSystem;
    private readonly UIManager uiManager;

    public PlayerTurnState(BuildingSystem buildingSystem, UIManager uiManager)
    {
        this.buildingSystem = buildingSystem;
        this.uiManager = uiManager;
    }

    public event Action StateComplete;
    public GameplayStateStatus GameplayStateStatus { get; set; }

    public void Activate()
    {
        buildingSystem.Activate();
        uiManager.EndTurnPresenter.ActivateButton(true);
        uiManager.EndTurnPresenter.TurnEnded += HandleTurnEnd;
    }

    private void HandleTurnEnd()
    {
        StateComplete?.Invoke();
    }

    public void OnPause()
    {
        buildingSystem.Deactivate();
    }

    public void OnUnpause()
    {
        buildingSystem.Activate();
    }

    public void Update()
    {
    }

    public void Deactivate()
    {
        buildingSystem.Deactivate();
        uiManager.EndTurnPresenter.ActivateButton(false);
        uiManager.EndTurnPresenter.TurnEnded -= HandleTurnEnd;
    }
}