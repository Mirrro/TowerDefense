using System.Collections.Generic;
using UnityEngine;

public class TowerFactory
{
    private const string path = "ViewContainer";
    
    private readonly EnemyManager enemyManager;
    private List<TowerPresenter> presenters = new ();

    public TowerFactory(EnemyManager enemyManager)
    {
        this.enemyManager = enemyManager;
    }
    
    public TowerPresenter CreateBasicTower(TowerModel model)
    {
        var container = Resources.Load<ViewContainer>(path);
        TowerPresenter presenter = new TowerPresenter(Object.Instantiate(container.TowerView, Vector3.zero, Quaternion.identity), model, enemyManager);
        presenter.Initialize();
        presenters.Add(presenter);
        return presenter;
    }

    public void Update()
    {
        foreach (var towerPresenter in presenters)
        {
            towerPresenter.Update();
        }
    }
}