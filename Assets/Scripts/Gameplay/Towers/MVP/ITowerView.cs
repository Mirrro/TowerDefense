using System;
using UnityEngine;

namespace Gameplay.Towers.MVP
{
    public interface ITowerView
    {
        public void FireAtTarget(Transform target, Action callback);
    }
}