using System.Collections.Generic;
using System.Linq;
using Gameplay.Enemies;
using Gameplay.Towers.MVP;
using Zenject;

namespace Gameplay.Towers.Strategies
{
    public class TowerDetectingStrategy : ITowerDetectingStrategy
    {
        private readonly EnemyManager enemyManager;

        private TowerPresenter towerPresenter;

        private TowerDetectingStrategy(EnemyManager enemyManager)
        {
            this.enemyManager = enemyManager;
        }

        public void Initialize(TowerPresenter towerPresenter)
        {
            this.towerPresenter = towerPresenter;
        }

        public List<IEnemyPresenter> Detect()
        {
            return enemyManager.FindEnemiesOnGrid(towerPresenter.TowerPosition, towerPresenter.TowerRange).ToList();
        }
        
        public class Factory : PlaceholderFactory<TowerDetectingStrategy>
        {
            
        }
    }
}