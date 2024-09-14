using System.Collections.Generic;
using Gameplay.Enemies;
using Gameplay.Towers.MVP;
using Zenject;

namespace Gameplay.Towers.Strategies
{
    public class MultipleTargetAttackingStrategy : ITowerAttackingStrategy
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
            enemyPresenter.ReceiveDamage(towerPresenter.TowerDamage);
        }
        
        public class Factory : PlaceholderFactory<MultipleTargetAttackingStrategy>
        {
            
        }
    }
}