using System;
using Cysharp.Threading.Tasks;
using Gameplay.Enemies;
using Gameplay.Systems;

namespace Gameplay.GameplayLoop.GameplayStateMachine.GameplayStates
{
    public class EnemyTurnState : IGameplayState
    {
        private readonly EnemyDeathRewardSystem deathRewardSystem;
        private readonly EnemyReachGoalSystem enemyReachGoalSystem;
        private readonly EnemyManager enemyManager;
        public event Action StateComplete;
        public GameplayStateStatus GameplayStateStatus { get; set; }
        private bool isWaveComplete;

        public EnemyTurnState(EnemyDeathRewardSystem deathRewardSystem, EnemyManager enemyManager, EnemyReachGoalSystem enemyReachGoalSystem)
        {
            this.deathRewardSystem = deathRewardSystem;
            this.enemyManager = enemyManager;
            this.enemyReachGoalSystem = enemyReachGoalSystem;
        }

        public void Activate()
        {
            deathRewardSystem.Activate();
            enemyReachGoalSystem.Activate();
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
            enemyReachGoalSystem.Deactivate();
            isWaveComplete = false;
        }
    }
}