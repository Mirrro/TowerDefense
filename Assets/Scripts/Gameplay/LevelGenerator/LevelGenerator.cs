using System.Linq;
using Gameplay.Blocks.Ground;
using Gameplay.Blocks.Obstacle;
using Gameplay.Blocks.Water;
using Gameplay.Enemies;
using Gameplay.Grid;
using Gameplay.Util;
using UnityEngine;
using Zenject;

namespace Gameplay.LevelGenerator
{
    public class LevelGenerator
    {
        [Inject] private readonly EnemyManager enemyManager;
        [Inject] private readonly PresenterFactory presenterFactory;
    
        private PathFinding.PathFinding pathFinding = new ();
        private ConvertService convertService = new ();
    
        public void PopulateGrid(Grid.Grid grid)
        {
            float seed = Random.Range(0f, 10f);
        
            for (int x = 0; x < grid.GridNodes.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GridNodes.GetLength(1); y++)
                {
                    if (x > 20 && x < 30 && y > 20 && y < 30)
                    {
                        var model = new GroundBlockModel();
                        model.Position = new Vector3(x, 0, y);
                        var presenter = presenterFactory.CreateGroundBlockPresenter(model);
                        presenter.Initialize();
                        grid.GridNodes[x,y].AddGirdElement(presenter);
                        continue;
                    }
                
                    float xCoord = seed + (float) x / grid.Size.x * 5f;
                    float yCoord = seed + (float) y / grid.Size.x * 5f;
                    float perlinNoise = Mathf.PerlinNoise(xCoord, yCoord);
                
                    if (perlinNoise <= .3f && EnsurePath(grid, new Vector2Int(x,y)))
                    {
                        var model = new WaterBlockModel();
                        model.Position = new Vector3(x, 0, y);
                        var presenter = presenterFactory.CreateWaterBlockPresenter(model);
                        presenter.Initialize();
                        grid.GridNodes[x,y].AddGirdElement(presenter);
                    }
                    else if (perlinNoise >= .7f && EnsurePath(grid, new Vector2Int(x,y)))
                    {
                        var model = new ObstacleBlockModel();
                        model.Position = new Vector3(x, 0, y);
                        var presenter = presenterFactory.CreateObstacleBlockPresenter(model);
                        presenter.Initialize();
                        grid.GridNodes[x,y].AddGirdElement(presenter);
                    }
                    else
                    {
                        var model = new GroundBlockModel();
                        model.Position = new Vector3(x, 0, y);
                        var presenter = presenterFactory.CreateGroundBlockPresenter(model);
                        presenter.Initialize();
                        grid.GridNodes[x,y].AddGirdElement(presenter);
                    }
                }
            }
        }
    
        private bool EnsurePath(Grid.Grid grid, Vector2Int position)
        {
            var convertedGrid = convertService.ConvertGridNodes(grid.GridNodes);
            convertedGrid[position.x, position.y].IsWalkable = false;
            var path = pathFinding.GetPath(convertedGrid, enemyManager.StartPos, enemyManager.EndPos);
            return path.Any();
        }
    }
}