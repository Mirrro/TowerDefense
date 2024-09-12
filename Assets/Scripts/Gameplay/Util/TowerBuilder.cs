using System.Collections.Generic;
using Gameplay.Towers.MVP;
using Gameplay.Towers.StateMachine;
using Gameplay.Towers.Strategies;
using UnityEngine;
using Zenject;

public class TowerBuilder : ITickable
{
    private const string path = "ViewContainer";
    private TowerPresenter.Factory towerFactory;
    private readonly SingleTargetAttackingStrategy.Factory singleTargetAttackStrategyFactory;
    private readonly TowerDetectingStrategy.Factory towerDetectingStrategyFactory;
    private readonly SimpleSortingStrategy.Factory simpleSortingStrategyFactory;
    private readonly TowerCooldownStrategy.Factory cooldownStrategyFactory;
    private List<TowerPresenter> towerPresenters = new();

    public TowerBuilder(TowerPresenter.Factory towerFactory, 
        SingleTargetAttackingStrategy.Factory singleTargetAttackStrategyFactory,
        TowerDetectingStrategy.Factory towerDetectingStrategyFactory,
        SimpleSortingStrategy.Factory simpleSortingStrategyFactory,
        TowerCooldownStrategy.Factory cooldownStrategyFactory)
    {
        this.towerFactory = towerFactory;
        this.singleTargetAttackStrategyFactory = singleTargetAttackStrategyFactory;
        this.towerDetectingStrategyFactory = towerDetectingStrategyFactory;
        this.simpleSortingStrategyFactory = simpleSortingStrategyFactory;
        this.cooldownStrategyFactory = cooldownStrategyFactory;
    }
    
    public TowerPresenter CreateBasicTower()
    {
        var container = Resources.Load<ViewContainer>(path);
        
        TowerPresenter towerPresenter = towerFactory.Create(
            CreateBasicTowerStateMachine(),
            Object.Instantiate(container.TowerView, Vector3.zero, Quaternion.identity), 
            new TowerModel(Vector3.zero, 2, 10, .5f));
        towerPresenter.Initialize();
        towerPresenters.Add(towerPresenter);
        return towerPresenter;
    }

    public TowerPresenter CreateSpecialTower()
    {
        var container = Resources.Load<ViewContainer>(path);
        
        TowerPresenter towerPresenter = towerFactory.Create(
            CreateBasicTowerStateMachine(),
            Object.Instantiate(container.TowerAView, Vector3.zero, Quaternion.identity), 
            new TowerModel(Vector3.zero, 4, 150, 2f));
        towerPresenter.Initialize();
        towerPresenters.Add(towerPresenter);
        return towerPresenter;
    }

    private TowerStateMachine CreateBasicTowerStateMachine()
    {
        TowerStateMachine towerStateMachine = new TowerStateMachine(
            new TowerStateAttack(
                singleTargetAttackStrategyFactory.Create(),
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