using Gameplay.Enemies;
using Gameplay.Player;

namespace Gameplay.Systems
{
    public class EnemyDeathRewardSystem
    {
        private readonly PlayerBank playerBank;
        private readonly EnemyManager enemyManager;

        public EnemyDeathRewardSystem(PlayerBank playerBank, EnemyManager enemyManager)
        {
            this.playerBank = playerBank;
            this.enemyManager = enemyManager;
        }

        public void Activate()
        {
            enemyManager.EnemyDied += HandleEnemyDeath;
        }

        public void Deactivate()
        {
            enemyManager.EnemyDied -= HandleEnemyDeath;
        }

        private void HandleEnemyDeath(IEnemyPresenter obj)
        {
            playerBank.AddMoney(25);
        }
    }
}