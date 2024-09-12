using System.Collections.Generic;
using Gameplay.Towers.MVP;

namespace Gameplay.Towers.Strategies
{
    public interface ITowerDetectingStrategy
    {
        public void Initialize(TowerPresenter towerPresenter);
        public List<IEnemyPresenter> Detect();
    }
}