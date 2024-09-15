using System.Collections.Generic;
using Gameplay.Enemies;
using Gameplay.Towers.MVP;
using Zenject;

namespace Gameplay.Towers.Strategies
{
    public class FreezeMultipleTargetAttackingStrategy : ITowerAttackingStrategy
    {
        private TowerPresenter towerPresenter;
        
        public void Initialize(TowerPresenter towerPresenter)
        {
            this.towerPresenter = towerPresenter;
        }

        public void Attack(List<IEnemyPresenter> enemyPresenters)
        {
            foreach (var enemyPresenter in enemyPresenters)
            {
                towerPresenter.FireAtTarget(enemyPresenter.GetTransform(), () => ApplyDamage(enemyPresenter));
            }
        }
        
        private void ApplyDamage(IEnemyPresenter enemyPresenter)
        {
            enemyPresenter.Freeze(towerPresenter.TowerFreezeTime);
        }
        
        public class Factory : PlaceholderFactory<FreezeMultipleTargetAttackingStrategy>
        {
            
        }
    }
}