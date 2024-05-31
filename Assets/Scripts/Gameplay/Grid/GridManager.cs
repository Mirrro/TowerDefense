using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager
{
    public Grid Grid => grid;
    private Grid grid;

    private PathFinding pathFinding = new ();
    private ConvertService convertService = new ();
    
    public GridManager(Vector2Int gridSize)
    {
        grid = new Grid();
        grid.Initialize(gridSize);
    }

    public List<Vector3> GetPath(Vector2Int start, Vector2Int end)
    {
        return pathFinding.GetPath(
                nodes: convertService.ConvertGridNodes(grid.GridNodes), 
                start: start, 
                end: end)
            .Select(x => new Vector3(x.X, 0, x.Y))
            .ToList();
    }

    public Vector2Int WorldToGridPosition(Vector3 worldPosition)
    {
        return new Vector2Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.z));
    }

    public bool IsInBound(Vector2Int gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < grid.Size.x &&
               gridPosition.y >= 0 && gridPosition.y < grid.Size.y;
    }
}