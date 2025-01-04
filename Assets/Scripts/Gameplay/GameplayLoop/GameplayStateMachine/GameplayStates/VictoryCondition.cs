using System;
using System.Linq;
using Gameplay.Enemies;
using Zenject;

namespace Gameplay.GameplayLoop.GameplayStateMachine.GameplayStates
{
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
                if (enemyManager.ActiveEnemies.All(enemy => !enemy.IsAlive))
                {
                    Victory?.Invoke();
                }
            }
        }
    }
}