using Gameplay.Enemies;
using Gameplay.Player;

namespace Gameplay.Systems
{
    public class EnemyReachGoalSystem
    {
        private readonly PlayerHealth playerHealth;
        private readonly EnemyManager enemyManager;

        public EnemyReachGoalSystem(PlayerHealth playerHealth, EnemyManager enemyManager)
        {
            this.playerHealth = playerHealth;
            this.enemyManager = enemyManager;
        }

        public void Activate()
        {
            enemyManager.EnemyReachedGoal += HandleEnemyDeath;
        }

        public void Deactivate()
        {
            enemyManager.EnemyReachedGoal -= HandleEnemyDeath;
        }

        private void HandleEnemyDeath(IEnemyPresenter enemyPresenter)
        {
            playerHealth.RemoveHealth(1);
        }
    }
}