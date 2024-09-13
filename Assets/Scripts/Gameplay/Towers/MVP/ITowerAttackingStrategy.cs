using System.Collections.Generic;
using Gameplay.Enemies;

namespace Gameplay.Towers.MVP
{
    public interface ITowerAttackingStrategy
    {
        public void Initialize(TowerPresenter towerPresenter);
        public void Attack(List<IEnemyPresenter> enemyPresenters);
    }
}