using System.Collections.Generic;
using Gameplay.Grid;
using UnityEngine.Pool;

namespace Gameplay.Enemies
{
    public class EnemyBuilder
    {
        private EnemyPresenter.Factory enemyFactory;
        private readonly GridManager gridManager;
        private List<EnemyPresenter> enemyPresenters = new();
        private EnemyViewPool enemyViewPool;

        public EnemyBuilder(EnemyPresenter.Factory enemyFactory, GridManager gridManager, EnemyViewPool enemyViewPool)
        {
            this.enemyFactory = enemyFactory;
            this.gridManager = gridManager;
            this.enemyViewPool = enemyViewPool;
        }
    
        public EnemyPresenterBuild CreateWarriorEnemy()
        {
            EnemyPresenter enemyPresenter = enemyFactory.Create(
                enemyViewPool.WarriorPool.Get(),
                new EnemyModel(200, 1),
                new EnemyMovementStrategy(gridManager),
                new EnemyDamageStrategy());
            enemyPresenters.Add(enemyPresenter);
            return new EnemyPresenterBuild(enemyPresenter, enemyViewPool.WarriorPool);
        }

        public EnemyPresenterBuild CreateMageEnemy()
        {
            EnemyPresenter enemyPresenter = enemyFactory.Create(
                enemyViewPool.MagePool.Get(),
                new EnemyModel(50, 2),
                new EnemyMovementStrategy(gridManager),
                new EnemyDamageStrategy());
            enemyPresenters.Add(enemyPresenter);
            return new EnemyPresenterBuild(enemyPresenter, enemyViewPool.MagePool);
        }
    }

    public class EnemyPresenterBuild
    {
        public EnemyPresenter Presenter;
        private IObjectPool<EnemyView> pool;

        public EnemyPresenterBuild(EnemyPresenter presenter, IObjectPool<EnemyView> pool)
        {
            Presenter = presenter;
            this.pool = pool;
        }

        public void Dispose()
        {
            Presenter.Dispose();
            pool.Release(Presenter.View as EnemyView);
        }
    }
}