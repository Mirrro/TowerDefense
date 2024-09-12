using Gameplay.Towers.MVP;
using Gameplay.Towers.Strategies;
using UnityEngine;

namespace Gameplay.Towers.StateMachine
{
    public class TowerStateAttack : ITowerState
    {
        private readonly EnemyManager enemyManager;
        private readonly TowerPresenter towerPresenter;
    
        private readonly ITowerAttackingStrategy towerAttackingStrategy;
        private readonly ITowerSortingStrategy towerSortingStrategy;
        private readonly ITowerCooldownStrategy towerCooldownStrategy;
        private readonly ITowerDetectingStrategy towerDetectingStrategy;

        public TowerStateAttack(
            ITowerAttackingStrategy towerAttackingStrategy,
            ITowerSortingStrategy towerSortingStrategy, 
            ITowerCooldownStrategy towerCooldownStrategy,
            ITowerDetectingStrategy towerDetectingStrategy)
        {
            this.towerAttackingStrategy = towerAttackingStrategy;
            this.towerSortingStrategy = towerSortingStrategy;
            this.towerCooldownStrategy = towerCooldownStrategy;
            this.towerDetectingStrategy = towerDetectingStrategy;
        }

        public void Initialize(TowerPresenter towerPresenter)
        {
            towerAttackingStrategy.Initialize(towerPresenter);
            towerSortingStrategy.Initialize(towerPresenter);
            towerCooldownStrategy.Initialize(towerPresenter);
            towerDetectingStrategy.Initialize(towerPresenter);
        }

        public void Enter()
        {
            Debug.Log("Entering Attack State");
        }

        public void Update()
        {
            if (towerCooldownStrategy.IsCooldown)
            {
                return;
            }

            var availableTargets = towerDetectingStrategy.Detect();
        
            if (availableTargets.Count > 0)
            {
                var prioritizedTargets = towerSortingStrategy.Sort(availableTargets);
                towerAttackingStrategy.Attack(prioritizedTargets);
                towerCooldownStrategy.Cooldown();
            }
        }

        public void Exit()
        {
            Debug.Log("Exiting Attack State");
        }
    }
}