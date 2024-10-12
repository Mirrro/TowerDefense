using Gameplay.Towers.MVP;
using Gameplay.Towers.Strategies;
using UnityEngine;

namespace Gameplay.Towers.StateMachine
{
    public class TowerStateAttack : ITowerState
    {
        private TowerPresenter towerPresenter;
    
        private readonly ITowerAttackComponent towerAttackComponent;
        private readonly ITowerSortingStrategy towerSortingStrategy;
        private readonly ITowerCooldownStrategy towerCooldownStrategy;
        private readonly ITowerDetectingStrategy towerDetectingStrategy;

        public TowerStateAttack(
            ITowerAttackComponent towerAttackComponent,
            ITowerSortingStrategy towerSortingStrategy, 
            ITowerCooldownStrategy towerCooldownStrategy,
            ITowerDetectingStrategy towerDetectingStrategy)
        {
            this.towerAttackComponent = towerAttackComponent;
            this.towerSortingStrategy = towerSortingStrategy;
            this.towerCooldownStrategy = towerCooldownStrategy;
            this.towerDetectingStrategy = towerDetectingStrategy;
        }

        public void Initialize(TowerPresenter towerPresenter)
        {
            this.towerPresenter = towerPresenter;
            towerAttackComponent.Initialize(towerPresenter);
        }

        public void Enter()
        {
            Debug.Log("Entering Attack State");
        }

        public void Update()
        {
            towerCooldownStrategy.Cooldown(ref towerPresenter.Model.ReloadTime, Time.deltaTime);
            if (towerPresenter.Model.ReloadTime > 0)
            {
                return;
            }

            var availableTargets = towerDetectingStrategy.Detect(towerPresenter.TowerPosition, towerPresenter.TowerRange);
        
            if (availableTargets.Count > 0)
            {
                towerSortingStrategy.Sort(ref availableTargets);
                towerAttackComponent.Attack(availableTargets);
                towerPresenter.Model.ReloadTime = towerPresenter.Model.MaxReloadTime;
            }
        }

        public void Exit()
        {
            Debug.Log("Exiting Attack State");
        }
    }
}