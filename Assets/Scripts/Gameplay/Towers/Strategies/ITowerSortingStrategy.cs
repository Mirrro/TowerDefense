using System.Collections.Generic;
using Gameplay.Towers.MVP;

namespace Gameplay.Towers.Strategies
{
    public interface ITowerSortingStrategy
    {
        public void Initialize(TowerPresenter towerPresenter);
        public List<IEnemyPresenter> Sort(List<IEnemyPresenter> targets);
    }
}