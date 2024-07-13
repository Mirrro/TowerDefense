using System;
using Zenject;

public class InitializationState : IGameplayState
{
    [Inject] private LevelGenerator levelGenerator;
    [Inject] private PlayerBank playerBank;
    [Inject] private UIManager uiManager;
    [Inject] private GridManager gridManager;
    
    public event Action StateComplete;
    public GameplayStateStatus GameplayStateStatus { get; set; }
    public void Activate()
    {
        levelGenerator.PopulateGrid(gridManager.Grid);
        playerBank.AddMoney(1000);
        uiManager.Initialize();
        StateComplete?.Invoke();
    }

    public void OnPause()
    {
        throw new NotImplementedException();
    }

    public void OnUnpause()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        
    }

    public void Deactivate()
    {
        
    }
}