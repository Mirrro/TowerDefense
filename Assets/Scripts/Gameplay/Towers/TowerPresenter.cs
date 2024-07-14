using System.Linq;
using UnityEngine;
using Zenject;

public class TowerPresenter : IGridElement
{
    [Inject] private readonly EnemyManager enemyManager;
    
    private readonly TowerModel model;
    private readonly TowerView view;

    public bool IsSolid => true;
    
    public TowerPresenter(TowerView view, TowerModel model)
    {
        this.model = model;
        this.view = view;
    }

    public void Tick()
    {
        if (isCooldown) return;
        
        // Check for enemies in range
        var enemiesOnGrid = enemyManager.FindEnemiesOnGrid(model.Position, model.Range);
        if (enemiesOnGrid.Any())
        {
            // Fire in the hole!
            var enemy = enemiesOnGrid.First();
            view.Fire(new[] {enemy.GetTransform()});
            enemy.ReceiveDamage(model.Damage);
            Cooldown();
        }
    }
    
    private bool isCooldown => Time.unscaledTime < model.LastTimeFired + model.ReloadTime;

    private void Cooldown()
    {
        model.LastTimeFired = Time.unscaledTime;
    }
    
    public void OnGridPosition(Vector3 position)
    {
        model.Position = position;
        view.SetPosition(model.Position);
    }

    public class Factory : PlaceholderFactory<TowerView, TowerModel, TowerPresenter>
    {
        
    }
}