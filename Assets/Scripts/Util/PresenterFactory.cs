using UnityEngine;

public static class PresenterFactory
{
    private const string path = "ViewContainer";
    public static WaterBlockPresenter CreateWaterBlockPresenter(WaterBlockModel model)
    {
        var container = Resources.Load<ViewContainer>(path);
        return new WaterBlockPresenter(Object.Instantiate(container.WaterBlockView, Vector3.zero, Quaternion.identity), model);
    }
    
    public static GroundBlockPresenter CreateGroundBlockPresenter(GroundBlockModel model)
    {
        var container = Resources.Load<ViewContainer>(path);
        return new GroundBlockPresenter(Object.Instantiate(container.GroundBlockView, Vector3.zero, Quaternion.identity), model);
    }
    
    public static ObstacleBlockPresenter CreateObstacleBlockPresenter(ObstacleBlockModel model)
    {
        var container = Resources.Load<ViewContainer>(path);
        return new ObstacleBlockPresenter(Object.Instantiate(container.ObstacleBlockView, Vector3.zero, Quaternion.identity), model);
    }
    
    public static EnemyPresenter CreateEnemy(EnemyModel model, GridManager gridManager)
    {
        var container = Resources.Load<ViewContainer>(path);
        return new EnemyPresenter(Object.Instantiate(container.EnemyView, Vector3.zero, Quaternion.identity), model, gridManager);
    }
    
    
}