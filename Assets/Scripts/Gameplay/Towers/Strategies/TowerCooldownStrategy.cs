using Gameplay.Towers.MVP;
using UnityEngine;
using Zenject;

namespace Gameplay.Towers.Strategies
{
    public class TowerCooldownStrategy : ITowerCooldownStrategy
    {
        private float startTime = 0;
        private TowerPresenter towerPresenter;

        public void Initialize(TowerPresenter towerPresenter)
        {
            this.towerPresenter = towerPresenter;
        }

        public bool IsCooldown => Time.time < startTime + towerPresenter.TowerReloadTime;

        public void Cooldown()
        {
            startTime = Time.time;
        }

        public class Factory : PlaceholderFactory<TowerCooldownStrategy>
        {
            
        }
    }
}