using System.Collections.Generic;
using System.Linq;
using Gameplay.Towers.MVP;
using UnityEngine;
using Zenject;

namespace Gameplay.Towers.Strategies
{
    public class SingleTargetAttackingStrategy : ITowerAttackingStrategy
    {
        private TowerPresenter towerPresenter;

        public void Initialize(TowerPresenter towerPresenter)
        {
            this.towerPresenter = towerPresenter;
        }

        public void Attack(List<IEnemyPresenter> enemyPresenters)
        {
            Debug.Log("Attacking");
            IEnemyPresenter target = enemyPresenters.First();
            if (target != null)
            {
                towerPresenter.FireAtTarget(target.GetTransform(), () => ApplyDamage(target));
            }
        }

        private void ApplyDamage(IEnemyPresenter enemyPresenter)
        {
            enemyPresenter.ReceiveDamage(towerPresenter.TowerDamage);
        }
        
        public class Factory : PlaceholderFactory<SingleTargetAttackingStrategy>
        {
            
        }
    }
}