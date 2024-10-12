using System.Collections.Generic;
using Gameplay.Enemies;
using UnityEngine;

namespace Gameplay.Towers.MVP
{
    public interface ITowerDetectingStrategy
    {
        public List<ITargetable> Detect(Vector3 position, int range);
    }
}