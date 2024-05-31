using System.Linq;
using UnityEngine;

public class TowerPresenter : IGridElement
{
    public bool IsSolid => true;
    
    private readonly TowerModel model;
    private readonly EnemyManager enemyManager;
    private readonly TowerView view;

    private bool isCooldown => Time.unscaledTime < model.LastTimeFired + model.ReloadTime;

    public TowerPresenter(TowerView view, TowerModel model, EnemyManager enemyManager)
    {
        this.model = model;
        this.enemyManager = enemyManager;
        this.view = view;
    }

    public void Initialize()
    {
        view.SetPosition(model.Position);
    }

    public void Update()
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

    private void Cooldown()
    {
        model.LastTimeFired = Time.unscaledTime;
    }
}