using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Blocks.Ground;
using UnityEngine;
using Zenject;

namespace Gameplay.Grid
{
    public class GridManager : IInitializable
    {
        public event Action ElementAdded;
        public event Action ElementRemoved;
        public Grid Grid => grid;
        private Grid grid;

        private PathFinding.PathFinding pathFinding = new ();
        private ConvertService convertService = new ();
    
    
        public void Initialize()
        {
            grid = new Grid();
            grid.Initialize(new Vector2Int(30,8));
            grid.ElementAdded += HandleElementAdded;
            grid.ElementRemoved += HandleElementRemoved;
        }

        private void HandleElementAdded()
        {
            ElementAdded?.Invoke();
        }
    
        private void HandleElementRemoved()
        {
            ElementRemoved?.Invoke();
        }

        public void ActivateBuildModeVisual()
        {
            foreach (var gridNode in grid.GridNodes)
            {
                foreach (var gridElement in gridNode.GridElements)
                {
                    if (gridElement is IBuildModeBlock buildModeBlock)
                    {
                        buildModeBlock.EnterBuildMode(.5f);
                    }
                }
            }
        }
        
        public void DeactivateBuildModeVisual()
        {
            foreach (var gridNode in grid.GridNodes)
            {
                foreach (var gridElement in gridNode.GridElements)
                {
                    if (gridElement is IBuildModeBlock buildModeBlock)
                    {
                        buildModeBlock.ExitBuildMode(.5f);
                    }
                }
            }
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
}