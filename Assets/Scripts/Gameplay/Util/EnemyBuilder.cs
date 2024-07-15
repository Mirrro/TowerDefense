using System.Collections.Generic;
using UnityEngine;

public class EnemyBuilder
{
    private const string path = "ViewContainer";
    private EnemyPresenter.Factory enemyFactory;
    private List<EnemyPresenter> towerPresenters = new();

    public EnemyBuilder(EnemyPresenter.Factory enemyFactory)
    {
        this.enemyFactory = enemyFactory;
    }
    
    public EnemyPresenter CreateBasicEnemy()
    {
        var container = Resources.Load<ViewContainer>(path);
        EnemyPresenter towerPresenter = enemyFactory.Create(Object.Instantiate(container.EnemyView, Vector3.zero, Quaternion.identity),
            new EnemyModel(200));
        towerPresenter.Initialize();
        towerPresenters.Add(towerPresenter);
        return towerPresenter;
    }
}