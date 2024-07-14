using Zenject;

public class GameplayLoop : IInitializable
{
    private readonly GameplayStateMachine gameplayStateMachine;
    private readonly PlayerTurnState playerTurnState;
    private readonly EnemyTurnState enemyTurnState;
    private readonly InitializationState initializationState;
    private readonly DefeatState defeatState;
    private readonly VictoryState victoryState;
    
    private readonly DefeatCondition defeatCondition;
    private readonly VictoryCondition victoryCondition;

    public GameplayLoop(GameplayStateMachine gameplayStateMachine, PlayerTurnState playerTurnState,
        EnemyTurnState enemyTurnState, InitializationState initializationState, 
        VictoryState victoryState, DefeatState defeatState, 
        DefeatCondition defeatCondition, VictoryCondition victoryCondition)
    {
        this.gameplayStateMachine = gameplayStateMachine;
        this.playerTurnState = playerTurnState;
        this.enemyTurnState = enemyTurnState;
        this.initializationState = initializationState;
        this.victoryState = victoryState;
        this.defeatState = defeatState;
        this.defeatCondition = defeatCondition;
        this.victoryCondition = victoryCondition;
    }
    
    public void Initialize()
    {
        initializationState.StateComplete += HandleInitializationStateComplete;
        playerTurnState.StateComplete += HandlePlayerStateComplete;
        enemyTurnState.StateComplete += HandleEnemyStateComplete;
        
        gameplayStateMachine.SwitchState(initializationState);
    }

    private void HandleInitializationStateComplete()
    {
        gameplayStateMachine.SwitchState(playerTurnState);
        defeatCondition.Defeat += HandleDefeat;
        victoryCondition.Victory += HandleVictory;
    }

    private void HandleDefeat()
    {
        defeatCondition.Defeat -= HandleDefeat;
        victoryCondition.Victory -= HandleVictory;
        gameplayStateMachine.SwitchState(defeatState);
    }
    
    private void HandleVictory()
    {
        defeatCondition.Defeat -= HandleDefeat;
        victoryCondition.Victory -= HandleVictory;
        gameplayStateMachine.SwitchState(victoryState);
    }

    private void HandlePlayerStateComplete()
    {
        gameplayStateMachine.SwitchState(enemyTurnState);
    }
    
    private void HandleEnemyStateComplete()
    {
        gameplayStateMachine.SwitchState(playerTurnState);
    }
}