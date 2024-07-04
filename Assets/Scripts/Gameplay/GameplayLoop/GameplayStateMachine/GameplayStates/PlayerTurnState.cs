using System;

public class PlayerTurnState : IGameplayState
{
    private readonly TowerBuildSystem towerBuildSystem;
    private readonly UIManager uiManager;
    private readonly BuildMenuItemsContainer container;

    public PlayerTurnState(TowerBuildSystem towerBuildSystem, UIManager uiManager, BuildMenuItemsContainer container)
    {
        this.towerBuildSystem = towerBuildSystem;
        this.uiManager = uiManager;
        this.container = container;
    }

    public event Action StateComplete;
    public GameplayStateStatus GameplayStateStatus { get; set; }

    public void Activate()
    {
        towerBuildSystem.SetItems(container.Items);
        towerBuildSystem.Activate();
        uiManager.EndTurnPresenter.ActivateButton(true);
        uiManager.EndTurnPresenter.TurnEnded += HandleTurnEnd;
    }

    private void HandleTurnEnd()
    {
        StateComplete?.Invoke();
    }

    public void OnPause()
    {
        towerBuildSystem.Deactivate();
    }

    public void OnUnpause()
    {
        towerBuildSystem.Activate();
    }

    public void Update()
    {
    }

    public void Deactivate()
    {
        towerBuildSystem.Deactivate();
        uiManager.EndTurnPresenter.ActivateButton(false);
        uiManager.EndTurnPresenter.TurnEnded -= HandleTurnEnd;
    }
}