using System.Collections.Generic;
using Gameplay.Towers.MVP;
using Gameplay.Towers.StateMachine;
using Gameplay.Towers.Strategies;
using Gameplay.Util;
using UnityEngine;
using Zenject;

namespace Gameplay.Towers
{
    public class TowerBuilder : ITickable
    {
        private const string path = "ViewContainer";
        private TowerPresenter.Factory towerFactory;
        private readonly SingleTargetAttackComponent.Factory singleTargetAttackStrategyFactory;
        private readonly MultipleTargetAttackComponent.Factory multipleTargetAttackStrategyFactory;
        private readonly FreezeMultipleTargetAttackComponent.Factory freezeMultipleTargetAttackingStrategy;
        private readonly TowerDetectingStrategy.Factory towerDetectingStrategyFactory;
        private readonly SimpleSortingStrategy.Factory simpleSortingStrategyFactory;
        private readonly TowerCooldownStrategy.Factory cooldownStrategyFactory;
        private List<TowerPresenter> towerPresenters = new();

        public TowerBuilder(TowerPresenter.Factory towerFactory, 
            SingleTargetAttackComponent.Factory singleTargetAttackStrategyFactory,
            MultipleTargetAttackComponent.Factory multipleTargetAttackStrategyFactory,
            FreezeMultipleTargetAttackComponent.Factory freezeMultipleTargetAttackingStrategy,
            TowerDetectingStrategy.Factory towerDetectingStrategyFactory,
            SimpleSortingStrategy.Factory simpleSortingStrategyFactory,
            TowerCooldownStrategy.Factory cooldownStrategyFactory)
        {
            this.towerFactory = towerFactory;
            this.singleTargetAttackStrategyFactory = singleTargetAttackStrategyFactory;
            this.multipleTargetAttackStrategyFactory = multipleTargetAttackStrategyFactory;
            this.freezeMultipleTargetAttackingStrategy = freezeMultipleTargetAttackingStrategy;
            this.towerDetectingStrategyFactory = towerDetectingStrategyFactory;
            this.simpleSortingStrategyFactory = simpleSortingStrategyFactory;
            this.cooldownStrategyFactory = cooldownStrategyFactory;
        }
    
        public TowerPresenter CreateElectricTower()
        {
            var container = Resources.Load<ViewContainer>(path);
        
            TowerPresenter towerPresenter = towerFactory.Create(
                CreateElectricTowerStateMachine(),
                Object.Instantiate(container.ElectricTowerView, Vector3.zero, Quaternion.identity), 
                new TowerModel(Vector3.zero, 1, 10, .5f));
            towerPresenter.Initialize();
            towerPresenters.Add(towerPresenter);
            return towerPresenter;
        }
        
        private TowerStateMachine CreateElectricTowerStateMachine()
        {
            TowerStateMachine towerStateMachine = new TowerStateMachine(
                new TowerStateAttack(
                    multipleTargetAttackStrategyFactory.Create(),
                    simpleSortingStrategyFactory.Create(),
                    cooldownStrategyFactory.Create(),
                    towerDetectingStrategyFactory.Create()));
            
            return towerStateMachine;
        }

        public TowerPresenter CreateFireTower()
        {
            var container = Resources.Load<ViewContainer>(path);
        
            TowerPresenter towerPresenter = towerFactory.Create(
                CreateFireTowerStateMachine(),
                Object.Instantiate(container.FireTowerView, Vector3.zero, Quaternion.identity), 
                new TowerModel(Vector3.zero, 4, 500, 3f));
            towerPresenter.Initialize();
            towerPresenters.Add(towerPresenter);
            return towerPresenter;
        }

        private TowerStateMachine CreateFireTowerStateMachine()
        {
            TowerStateMachine towerStateMachine = new TowerStateMachine(
                new TowerStateAttack(
                    singleTargetAttackStrategyFactory.Create(),
                    simpleSortingStrategyFactory.Create(),
                    cooldownStrategyFactory.Create(),
                    towerDetectingStrategyFactory.Create()));
            
            return towerStateMachine;
        }
        
        public TowerPresenter CreateIceTower()
        {
            var container = Resources.Load<ViewContainer>(path);
        
            TowerPresenter towerPresenter = towerFactory.Create(
                CreateIceTowerStateMachine(),
                Object.Instantiate(container.IceTowerView, Vector3.zero, Quaternion.identity), 
                new TowerModel(Vector3.zero, 1, 0, 2f));
            towerPresenter.Initialize();
            towerPresenters.Add(towerPresenter);
            return towerPresenter;
        }

        private TowerStateMachine CreateIceTowerStateMachine()
        {
            TowerStateMachine towerStateMachine = new TowerStateMachine(
                new TowerStateAttack(
                    freezeMultipleTargetAttackingStrategy.Create(),
                    simpleSortingStrategyFactory.Create(),
                    cooldownStrategyFactory.Create(),
                    towerDetectingStrategyFactory.Create()));
            
            return towerStateMachine;
        }
    
        public void Tick()
        {
            foreach (var towerPresenter in towerPresenters)
            {
                towerPresenter.Tick();
            }
        }
    }
}