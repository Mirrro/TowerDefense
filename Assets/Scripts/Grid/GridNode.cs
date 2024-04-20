using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridNode
{
    private List<IGridElement> gridElements = new List<IGridElement>();

    public Vector2Int Index => index;
    private readonly Vector2Int index;
    public GridNode(Vector2Int index)
    {
        this.index = index;
    }
    
    public bool IsSolid => gridElements.Any(x => x.IsSolid);

    public void AddGirdElement(IGridElement gridElement)
    {
        gridElements.Add(gridElement);
    }

    public void RemoveElement(IGridElement gridElement)
    {
        if (!gridElements.Contains(gridElement))
        {
            return;
        }
        
        gridElements.Remove(gridElement);
    }
}