using Gameplay.PathFinding;

namespace Gameplay.Grid
{
    public class ConvertService
    {
        public Node[,] ConvertGridNodes(GridNode[,] gridNodes)
        {
            var nodes = new Node[gridNodes.GetLength(0), gridNodes.GetLength(1)];
        
            for (var x = 0; x < gridNodes.GetLength(0); x++)
            {
                for (var y = 0; y < gridNodes.GetLength(1); y++)
                {
                    nodes[x, y] = new Node(x, y, isWalkable: !gridNodes[x,y].IsSolid);
                }
            }

            return nodes;
        }
    }
}