using Gameplay.Towers.MVP;
using UnityEngine;
using Zenject;

namespace Gameplay.Towers.Strategies
{
    public class TowerCooldownStrategy : ITowerCooldownStrategy
    {
        public void Cooldown(ref float cooldown, float deltaTime)
        {
            cooldown -= deltaTime;
        }

        public class Factory : PlaceholderFactory<TowerCooldownStrategy>
        {
            
        }
    }
}