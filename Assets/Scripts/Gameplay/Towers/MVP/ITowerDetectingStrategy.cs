using System.Collections.Generic;
using Gameplay.Enemies;

namespace Gameplay.Towers.MVP
{
    public interface ITowerDetectingStrategy
    {
        public void Initialize(TowerPresenter towerPresenter);
        public List<IEnemyPresenter> Detect();
    }
}