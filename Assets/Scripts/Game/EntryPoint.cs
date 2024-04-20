using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2Int start;
    [SerializeField] private Vector2Int end;
    [SerializeField] private CameraView cameraView;
    [SerializeField] private GameObject indicatorPrefab;
    
    private readonly LevelGenerator levelGenerator = new ();
    private readonly MouseRayCast mouseRayCast = new ();
    private GridManager gridManager;
    private LineRenderer lineRenderer;
    private CameraPresenter cameraPresenter;
    private GameObject indicator;
    private EnemyManager enemyManager;
    private List<TowerPresenter> towerPresenters = new (); 

    private void Awake()
    {
        gridManager = new GridManager(gridSize);
        lineRenderer = GetComponent<LineRenderer>();
        cameraPresenter = new CameraPresenter(cameraView, new CameraModel());
        enemyManager = new EnemyManager(gridManager);
        cameraPresenter.Initialize();
        indicator = Instantiate(indicatorPrefab);
    }

    private void Start()
    {
        levelGenerator.PopulateGrid(gridManager.Grid);
        StartCoroutine(SpawnInterval(1, 100));
    }

    private IEnumerator SpawnInterval(float time, int count)
    {
        for (int i = 0; i < count; i++)
        {
            enemyManager.SpawnEnemy(start, end);
            yield return new WaitForSeconds(time);
        }
    }

    private void Update()
    {
        var path = gridManager.GetPath(start, end);
        lineRenderer.positionCount = path.Count;
        lineRenderer.SetPositions(path.ToArray().Select(position => position + Vector3.up * .3f).ToArray());
        
        if (mouseRayCast.TryGetPosition(out var hit))
        {
            var gridPos = gridManager.WorldToGridPosition(hit);
            indicator.transform.position = new Vector3(gridPos.x, 0, gridPos.y);
            
            if (Input.GetMouseButtonDown(0))
            {
                if (gridManager.Grid.GridNodes[gridPos.x, gridPos.y].IsSolid)
                {
                    return;
                }
                var model = new TowerModel(new Vector3(gridPos.x, 0, gridPos.y), 2, 10, 1);
                var presenter = PresenterFactory.CreateTower(model, enemyManager);
                presenter.Initialize();
                towerPresenters.Add(presenter);
                gridManager.Grid.GridNodes[gridPos.x, gridPos.y].AddGirdElement(presenter);
            }
        }

        foreach (var presenter in towerPresenters)
        {
            presenter.Update();
        }
    }
}

public class PlacementSystem
{
    private readonly GridManager gridManager;
    private readonly MouseRayCast mouseRayCast;

    public PlacementSystem(GridManager gridManager, MouseRayCast mouseRayCast)
    {
        this.gridManager = gridManager;
        this.mouseRayCast = mouseRayCast;
    }

    public void Update()
    {
        // if (mouseRayCast.TryGetPosition(out var hit))
        // {
        //     var gridPos = gridManager.WorldToGridPosition(hit);
        //     indicator.transform.position = new Vector3(gridPos.x, 0, gridPos.y);
        //     
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //         if (gridManager.Grid.GridNodes[gridPos.x, gridPos.y].IsSolid)
        //         {
        //             return;
        //         }
        //         var model = new TowerModel(new Vector3(gridPos.x, 0, gridPos.y), 2, 10, 1);
        //         var presenter = PresenterFactory.CreateTower(model, enemyManager);
        //         presenter.Initialize();
        //         towerPresenters.Add(presenter);
        //         gridManager.Grid.GridNodes[gridPos.x, gridPos.y].AddGirdElement(presenter);
        //     }
        // }
    }
}