using System;
using UnityEngine;

public class EnemyTurnState : IGameplayState
{
    private readonly EnemyDeathRewardSystem deathRewardSystem;
    private readonly EnemyManager enemyManager;
    public event Action StateComplete;
    public GameplayStateStatus GameplayStateStatus { get; set; }

    public EnemyTurnState(EnemyDeathRewardSystem deathRewardSystem, EnemyManager enemyManager)
    {
        this.deathRewardSystem = deathRewardSystem;
        this.enemyManager = enemyManager;
    }

    public void Activate()
    {
        deathRewardSystem.Activate();
        enemyManager.SpawnEnemy(new Vector2Int(0,0), new Vector2Int(29,14));
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
        if (enemyManager.ActiveEnemiesCount <= 0)
        {
            StateComplete?.Invoke();
        }
    }

    public void Deactivate()
    {
        deathRewardSystem.Deactivate();
    }
}