using UnityEngine;

public class TowerFactory
{
    private const string path = "ViewContainer";
    
    private readonly EnemyManager enemyManager;

    public TowerFactory(EnemyManager enemyManager)
    {
        this.enemyManager = enemyManager;
    }
    
    public TowerPresenter CreateTower(TowerModel model)
    {
        var container = Resources.Load<ViewContainer>(path);
        TowerPresenter presenter = new TowerPresenter(Object.Instantiate(container.TowerView, Vector3.zero, Quaternion.identity), model, enemyManager);
        presenter.Initialize();
        return presenter;
    }
}