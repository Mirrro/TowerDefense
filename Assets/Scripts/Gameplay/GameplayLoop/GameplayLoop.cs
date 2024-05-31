using UnityEngine;

public class GameplayLoop
{
    private readonly GameplayStateMachine gameplayStateMachine;
    private readonly PlayerTurnState playerTurnState;
    private readonly EnemyTurnState enemyTurnState;

    public GameplayLoop(GameplayStateMachine gameplayStateMachine, PlayerTurnState playerTurnState,
        EnemyTurnState enemyTurnState)
    {
        this.gameplayStateMachine = gameplayStateMachine;
        this.playerTurnState = playerTurnState;
        this.enemyTurnState = enemyTurnState;
    }

    public void Start()
    {
        gameplayStateMachine.SwitchState(playerTurnState);
        playerTurnState.StateComplete += HandlePlayerStateComplete;
        enemyTurnState.StateComplete += HandleEnemyStateComplete;
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

    public void Update()
    {
        gameplayStateMachine.Update();
    }
}