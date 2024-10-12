using System.Collections.Generic;
using System.Linq;
using Gameplay.Enemies;
using Gameplay.Towers.MVP;
using UnityEngine;
using Zenject;

namespace Gameplay.Towers.Strategies
{
    public class TowerDetectingStrategy : ITowerDetectingStrategy
    {
        private readonly EnemyManager enemyManager;

        private TowerDetectingStrategy(EnemyManager enemyManager)
        {
            this.enemyManager = enemyManager;
        }

        public List<ITargetable> Detect(Vector3 position, int range)
        {
            return enemyManager.FindEnemiesOnGrid(position, range).OfType<ITargetable>().ToList();
        }
        
        public class Factory : PlaceholderFactory<TowerDetectingStrategy>
        {
            
        }
    }
}