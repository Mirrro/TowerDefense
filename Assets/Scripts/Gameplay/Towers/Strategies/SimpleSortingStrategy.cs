using System.Collections.Generic;
using Gameplay.Towers.MVP;
using Zenject;

namespace Gameplay.Towers.Strategies
{
    public class SimpleSortingStrategy : ITowerSortingStrategy
    {
        public void Initialize(TowerPresenter _)
        {
            
        }

        public List<IEnemyPresenter> Sort(List<IEnemyPresenter> targets)
        {
            return targets;
        }
        
        public class Factory : PlaceholderFactory<SimpleSortingStrategy>
        {
            
        }
    }
}