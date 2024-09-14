using UnityEngine;

namespace Gameplay.Grid
{
    public interface IGridElement
    {
        bool IsSolid { get; }

        public void OnGridPosition(Vector3 position);
    }
    
    public interface IPlaceable : IGridElement
    {
        public void HoverGridPosition(Vector3 position);
    }
}