using System.Collections.Generic;
using System.Linq;
using Gameplay.Enemies;
using Gameplay.Towers.MVP;
using Zenject;

namespace Gameplay.Towers.Strategies
{
    public class SingleTargetAttackComponent : ITowerAttackComponent
    {
        private TowerPresenter towerPresenter;
        
        public void Initialize(TowerPresenter towerPresenter)
        {
            this.towerPresenter = towerPresenter;
        }

        public void Attack(List<ITargetable> targetables)
        {
            ITargetable target = targetables.FirstOrDefault(x => x.CanBeTargeted);
            if (target != null)
            {
                towerPresenter.View.FireAtTarget(target.GetTarget(), () => ApplyDamage(target));
            }
        }

        private void ApplyDamage(ITargetable targetable)
        {
            if (targetable is IDamageable damageable)
            {
                damageable.ReceiveDamage(towerPresenter.Model.Damage);
            }
        }
        
        public class Factory : PlaceholderFactory<SingleTargetAttackComponent>
        {
            
        }
    }
}