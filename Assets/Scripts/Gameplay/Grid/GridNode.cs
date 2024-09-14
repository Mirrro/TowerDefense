using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Grid
{
    public class GridNode
    {
        public event Action ElementAdded;
        public event Action ElementRemoved;
        private List<IGridElement> gridElements = new List<IGridElement>();

        public Vector2Int Index => index;
        public List<IGridElement> GridElements => gridElements;
        private readonly Vector2Int index;
        public GridNode(Vector2Int index)
        {
            this.index = index;
        }
    
        public bool IsSolid => gridElements.Any(x => x.IsSolid);

        public void AddGirdElement(IGridElement gridElement)
        {
            gridElements.Add(gridElement);
            gridElement.OnGridPosition(new Vector3(index.x, 0, index.y));
            ElementAdded?.Invoke();
        }

        public void RemoveElement(IGridElement gridElement)
        {
            if (!gridElements.Contains(gridElement))
            {
                return;
            }
        
            gridElements.Remove(gridElement);
            ElementRemoved?.Invoke();
        }
    }
}