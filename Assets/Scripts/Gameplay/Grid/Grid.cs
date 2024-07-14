using System;
using UnityEngine;

public class Grid
{
    public event Action ElementAdded;
    public event Action ElementRemoved;
    public GridNode[,] GridNodes => gridNodes;
    private GridNode[,] gridNodes;
    public Vector2Int Size => size;
    private Vector2Int size;

    public void Initialize(Vector2Int size)
    {
        this.size = size;
        Populate();
    }

    private void Populate()
    {
        gridNodes = new GridNode[size.x, size.y];
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                gridNodes[x, y] = new GridNode(new Vector2Int(x,y));
                gridNodes[x, y].ElementAdded += HandleElementAdded;
                gridNodes[x, y].ElementRemoved += HandleElementRemoved;
            }
        }
    }

    private void HandleElementAdded()
    {
        ElementAdded?.Invoke();
    }
    
    private void HandleElementRemoved()
    {
        ElementRemoved?.Invoke();
    }
}