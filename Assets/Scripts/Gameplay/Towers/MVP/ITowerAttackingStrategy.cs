using System.Collections.Generic;
using Gameplay.Towers.MVP;

namespace Gameplay.Towers.Strategies
{
    public interface ITowerAttackingStrategy
    {
        public void Initialize(TowerPresenter towerPresenter);
        public void Attack(List<IEnemyPresenter> enemyPresenters);
    }
}