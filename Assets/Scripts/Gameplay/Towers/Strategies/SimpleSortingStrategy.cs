using System.Collections.Generic;
using Gameplay.Enemies;
using Gameplay.Towers.MVP;
using Zenject;

namespace Gameplay.Towers.Strategies
{
    public class SimpleSortingStrategy : ITowerSortingStrategy
    {
        public void Sort(ref List<ITargetable> targets)
        {
            
        }
        
        public class Factory : PlaceholderFactory<SimpleSortingStrategy>
        {
            
        }
    }
}