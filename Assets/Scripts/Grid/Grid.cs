using UnityEngine;

public class Grid
{
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
            }
        }
    }
}