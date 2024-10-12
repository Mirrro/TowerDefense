using System.Collections.Generic;
using Gameplay.Enemies;
using Gameplay.Towers.MVP;

namespace Gameplay.Towers.Strategies
{
    public interface ITowerSortingStrategy
    {
        public void Sort(ref List<ITargetable> targets);
    }
}