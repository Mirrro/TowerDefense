using System.Collections.Generic;
using System.Linq;
using Gameplay.Enemies;
using Gameplay.Towers.MVP;
using Zenject;

namespace Gameplay.Towers.Strategies
{
    public class MultipleTargetAttackComponent : ITowerAttackComponent
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
                towerPresenter.View.FireAtTarget(targetable.GetTarget(), () => TryApplyDamageToTarget(targetable));
            }
        }
        
        private void TryApplyDamageToTarget(ITargetable targetable)
        {
            if (targetable is IDamageable damageable)
            {
                damageable.ReceiveDamage(towerPresenter.TowerDamage);
            }
        }
        
        public class Factory : PlaceholderFactory<MultipleTargetAttackComponent>
        {
            
        }
    }
}