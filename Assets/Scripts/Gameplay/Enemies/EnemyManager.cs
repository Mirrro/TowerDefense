using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager
{
    public event Action<EnemyPresenter> EnemyDied;
    
    private readonly GridManager gridManager;
    public int ActiveEnemiesCount => activeEnemies.Count;
    private List<EnemyPresenter> activeEnemies = new ();

    public EnemyManager(GridManager gridManager)
    {
        this.gridManager = gridManager;
    }

    public void SpawnEnemy(Vector2Int startPos, Vector2Int endPosition)
    {
        var enemy = PresenterFactory.CreateEnemy(new EnemyModel(100), gridManager);
        enemy.Initialize();
        enemy.SetPosition(new Vector3(startPos.x, 0.3f, startPos.y));
        enemy.Move(endPosition);
        enemy.Died.AddListener(() => HandleDeath(enemy));
        activeEnemies.Add(enemy);
    }

    private void HandleDeath(EnemyPresenter enemyPresenter)
    {
        activeEnemies.Remove(enemyPresenter);
        EnemyDied?.Invoke(enemyPresenter);
    }

    public IEnumerable<EnemyPresenter> FindEnemiesOnGrid(Vector3 position, int radius)
    {
        return activeEnemies.Where(enemy => IsInRadius(
            origin: gridManager.WorldToGridPosition(position),
            radius: radius,
            target: gridManager.WorldToGridPosition(enemy.GetPosition())));
    }

    private bool IsInRadius(Vector2Int origin, int radius, Vector2Int target)
    {
        int distance = Math.Abs(target.x - origin.x) + Math.Abs(target.y - origin.y);
        return distance <= radius;
    }
}