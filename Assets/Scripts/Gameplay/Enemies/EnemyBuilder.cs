using System.Collections.Generic;
using Gameplay.Grid;
using Gameplay.Util;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class EnemyBuilder
    {
        private const string path = "ViewContainer";
        private EnemyPresenter.Factory enemyFactory;
        private readonly GridManager gridManager;
        private List<IEnemyPresenter> enemyPresenters = new();

        public EnemyBuilder(EnemyPresenter.Factory enemyFactory, GridManager gridManager)
        {
            this.enemyFactory = enemyFactory;
            this.gridManager = gridManager;
        }
    
        public EnemyPresenter CreateBasicEnemy()
        {
            var container = Resources.Load<ViewContainer>(path);
            EnemyPresenter enemyPresenter = enemyFactory.Create(
                Object.Instantiate(container.EnemyView, Vector3.zero, Quaternion.identity),
                new EnemyModel(200, 1),
                new EnemyMovementStrategy(gridManager),
                new EnemyDamageStrategy());
            enemyPresenters.Add(enemyPresenter);
            return enemyPresenter;
        }
    }
}