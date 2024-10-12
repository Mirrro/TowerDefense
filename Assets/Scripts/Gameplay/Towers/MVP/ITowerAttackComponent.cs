using System.Collections.Generic;
using Gameplay.Enemies;

namespace Gameplay.Towers.MVP
{
    public interface ITowerAttackComponent
    {
        public void Initialize(TowerPresenter towerPresenter);
        public void Attack(List<ITargetable> targetables);
    }
}