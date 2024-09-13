using System.Collections.Generic;
using Gameplay.Enemies;
using UnityEngine;

namespace Gameplay.Util
{
    public class EnemyBuilder
    {
        private const string path = "ViewContainer";
        private EnemyPresenter.Factory enemyFactory;
        private List<IEnemyPresenter> enemyPresenters = new();

        public EnemyBuilder(EnemyPresenter.Factory enemyFactory)
        {
            this.enemyFactory = enemyFactory;
        }
    
        public EnemyPresenter CreateBasicEnemy()
        {
            var container = Resources.Load<ViewContainer>(path);
            EnemyPresenter enemyPresenter = enemyFactory.Create(Object.Instantiate(container.EnemyView, Vector3.zero, Quaternion.identity),
                new EnemyModel(200));
            enemyPresenter.Initialize();
            enemyPresenters.Add(enemyPresenter);
            return enemyPresenter;
        }
    }
}