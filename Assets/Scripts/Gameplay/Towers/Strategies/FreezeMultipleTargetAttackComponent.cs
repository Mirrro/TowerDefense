using System.Collections.Generic;
using System.Linq;
using Gameplay.Enemies;
using Gameplay.Towers.MVP;
using Zenject;

namespace Gameplay.Towers.Strategies
{
    public class FreezeMultipleTargetAttackComponent : ITowerAttackComponent
    {
        private TowerPresenter towerPresenter;

        public void Initialize(TowerPresenter towerPresenter)
        {
            this.towerPresenter = towerPresenter;
        }

        public void Attack(List<ITargetable> targetables)
        {
            foreach (var targetable in targetables.Where(targetable => targetable.CanBeTargeted))
            {
                towerPresenter.View.FireAtTarget(targetable.GetTarget(), () => FreezeTarget(targetable));
            }
        }
        
        private void FreezeTarget(ITargetable targetable)
        {
            if (targetable is IAffectable affectable)
            {
                affectable.AddEffect(new StunEnemyEffect());
            }
        }
        
        public class Factory : PlaceholderFactory<FreezeMultipleTargetAttackComponent>
        {
            
        }
    }
}