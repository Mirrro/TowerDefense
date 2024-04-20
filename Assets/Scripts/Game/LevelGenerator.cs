using UnityEngine;

public class LevelGenerator
{
    public void PopulateGrid(Grid grid)
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
                    var presenter = PresenterFactory.CreateGroundBlockPresenter(model);
                    presenter.Initialize();
                    grid.GridNodes[x,y].AddGirdElement(presenter);
                    continue;
                }
                
                float xCoord = seed + (float) x / grid.Size.x * 5f;
                float yCoord = seed + (float) y / grid.Size.x * 5f;
                float perlinNoise = Mathf.PerlinNoise(xCoord, yCoord);
                
                if (perlinNoise <= .2f)
                {
                    var model = new WaterBlockModel();
                    model.Position = new Vector3(x, 0, y);
                    var presenter = PresenterFactory.CreateWaterBlockPresenter(model);
                    presenter.Initialize();
                    grid.GridNodes[x,y].AddGirdElement(presenter);
                }
                else if (perlinNoise <= .7f)
                {
                    var model = new GroundBlockModel();
                    model.Position = new Vector3(x, 0, y);
                    var presenter = PresenterFactory.CreateGroundBlockPresenter(model);
                    presenter.Initialize();
                    grid.GridNodes[x,y].AddGirdElement(presenter);
                }
                else
                {
                    var model = new ObstacleBlockModel();
                    model.Position = new Vector3(x, 0, y);
                    var presenter = PresenterFactory.CreateObstacleBlockPresenter(model);
                    presenter.Initialize();
                    grid.GridNodes[x,y].AddGirdElement(presenter);
                }
            }
        }
    }
}