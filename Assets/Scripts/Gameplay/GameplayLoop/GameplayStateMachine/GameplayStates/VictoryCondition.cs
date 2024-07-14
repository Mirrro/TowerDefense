using System;
using Zenject;

public class VictoryCondition : IInitializable, ITickable
{
    [Inject] private EnemyManager enemyManager;

    public event Action Victory;
    private bool isFinalWave;
    
    public void Initialize()
    {
        enemyManager.FinalWaveSend += HandleFinalWaveSend;
    }

    private void HandleFinalWaveSend()
    {
        isFinalWave = true;
    }

    public void Tick()
    {
        if (isFinalWave)
        {
            if (enemyManager.ActiveEnemiesCount <= 0)
            {
                Victory?.Invoke();
            }
        }
    }
}