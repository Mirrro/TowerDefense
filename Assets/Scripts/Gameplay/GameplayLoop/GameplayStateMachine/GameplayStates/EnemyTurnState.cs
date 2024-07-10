using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EnemyTurnState : IGameplayState
{
    private readonly EnemyDeathRewardSystem deathRewardSystem;
    private readonly EnemyManager enemyManager;
    public event Action StateComplete;
    public GameplayStateStatus GameplayStateStatus { get; set; }
    private bool isWaveComplete;

    public EnemyTurnState(EnemyDeathRewardSystem deathRewardSystem, EnemyManager enemyManager)
    {
        this.deathRewardSystem = deathRewardSystem;
        this.enemyManager = enemyManager;
    }

    public void Activate()
    {
        deathRewardSystem.Activate();
        enemyManager.SendNextWave(OnWaveComplete).Forget();
    }

    private void OnWaveComplete()
    {
        isWaveComplete = true;
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
        if (isWaveComplete)
        {
            if (enemyManager.ActiveEnemiesCount <= 0)
            {
                StateComplete?.Invoke();
            }
        }
    }

    public void Deactivate()
    {
        deathRewardSystem.Deactivate();
        isWaveComplete = false;
    }
}