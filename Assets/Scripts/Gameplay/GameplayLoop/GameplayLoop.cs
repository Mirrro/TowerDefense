using UnityEngine;
using Zenject;

public class GameplayLoop : IInitializable
{
    private readonly GameplayStateMachine gameplayStateMachine;
    private readonly PlayerTurnState playerTurnState;
    private readonly EnemyTurnState enemyTurnState;
    private readonly InitializationState initializationState;

    public GameplayLoop(GameplayStateMachine gameplayStateMachine, PlayerTurnState playerTurnState,
        EnemyTurnState enemyTurnState, InitializationState initializationState)
    {
        this.gameplayStateMachine = gameplayStateMachine;
        this.playerTurnState = playerTurnState;
        this.enemyTurnState = enemyTurnState;
        this.initializationState = initializationState;
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
    }

    private void HandlePlayerStateComplete()
    {
        if (!VictoryCondition())
        {
            gameplayStateMachine.SwitchState(enemyTurnState);
        }
        else
        {
            Debug.Log("You won! Unfortunately,there is no state for that yet.");
        }
    }
    
    private void HandleEnemyStateComplete()
    {
        gameplayStateMachine.SwitchState(playerTurnState);
    }

    private bool VictoryCondition()
    {
        return false;
    }
}