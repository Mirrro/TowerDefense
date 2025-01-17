using System.Linq;
using Gameplay.Blocks.Water;
using Gameplay.Enemies;
using Gameplay.Grid;
using Gameplay.Player;
using UnityEngine;
using Zenject;

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

        private void HandleEnemyDeath()
        {
            playerHealth.RemoveHealth(1);
        }
    }

    // Build upon this system and make grid handle IsSolid based on these systems. Have the check run on before evaluating
    public class BridgesMakeWaterWalkableSystem : IInitializable
    {
        private readonly GridManager gridManager;

        public BridgesMakeWaterWalkableSystem(GridManager gridManager)
        {
            this.gridManager = gridManager;
        }
        
        public void Initialize()
        {
            gridManager.ElementAdded += Check;
        }

        public void Check()
        {
            foreach (var gridNode in gridManager.Grid.GridNodes)
            {
                var waterBlocks = gridNode.GridElements.Where(x => x is WaterBlockPresenter).ToArray();
                var bridgeBlocks = gridNode.GridElements.Where(x => x is BridgePresenter).ToArray();
                
                if (waterBlocks.Any() && bridgeBlocks.Any())
                {
                    foreach (var waterBlock in waterBlocks)
                    {
                        WaterBlockPresenter presenter = waterBlock as WaterBlockPresenter;
                        presenter?.SetWalkable(true);
                        Debug.Log("Water Block Presenter is walkable");
                    }
                }
            }
        }
    }
}