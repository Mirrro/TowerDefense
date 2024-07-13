using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TowerBuilder : ITickable
{
    private const string path = "ViewContainer";
    private TowerPresenter.Factory towerFactory;
    private List<TowerPresenter> towerPresenters = new();

    public TowerBuilder(TowerPresenter.Factory towerFactory)
    {
        this.towerFactory = towerFactory;
    }
    
    public TowerPresenter CreateBasicTower()
    {
        var container = Resources.Load<ViewContainer>(path);
        TowerPresenter towerPresenter = towerFactory.Create(Object.Instantiate(container.TowerView, Vector3.zero, Quaternion.identity),
            new TowerModel(Vector3.zero, 10, 10, 1));
        towerPresenters.Add(towerPresenter);
        return towerPresenter;
    }

    public void Tick()
    {
        foreach (var towerPresenter in towerPresenters)
        {
            towerPresenter.Tick();
        }
    }
}