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
    [SerializeField] private UIViewReferences uiViewReferences;
    
    private readonly LevelGenerator levelGenerator = new ();
    private readonly MouseRayCast mouseRayCast = new ();
    private GridManager gridManager;
    private LineRenderer lineRenderer;
    private CameraPresenter cameraPresenter;
    private EnemyManager enemyManager;
    private GridInteraction gridInteraction;
    private GameObject indicator;
    private TowerFactory towerFactory;
    private BuildingSystem buildingSystem;
    private PlayerBank playerBank;
    private EnemyDeathRewardSystem enemyDeathRewardSystem;
    private UIManager uiManager;

    private List<TowerPresenter> presenters = new List<TowerPresenter>();

    private void Awake()
    {
        gridManager = new GridManager(gridSize);
        lineRenderer = GetComponent<LineRenderer>();
        enemyManager = new EnemyManager(gridManager);
        cameraPresenter = new CameraPresenter(cameraView, new CameraModel());
        gridInteraction = new GridInteraction(gridManager, mouseRayCast);
        towerFactory = new TowerFactory(enemyManager);
        playerBank = new PlayerBank();
        buildingSystem = new BuildingSystem(gridManager, gridInteraction, playerBank, towerFactory);
        enemyDeathRewardSystem = new EnemyDeathRewardSystem(playerBank, enemyManager);
        uiManager = new UIManager(uiViewReferences, playerBank);
    }

    private void Start()
    {
        enemyDeathRewardSystem.Initialize();
        cameraPresenter.Initialize();
        buildingSystem.Activate();
        levelGenerator.PopulateGrid(gridManager.Grid);
        playerBank.AddMoney(1000);
        StartCoroutine(SpawnInterval(1, 100));
        buildingSystem.TowerBuild += HandleTowerBuild;
        uiManager.Initialize();
    }

    private void HandleTowerBuild(TowerPresenter obj)
    {
        presenters.Add(obj);
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
        
        gridInteraction.Update();

        foreach (var towerPresenter in presenters)
        {
            towerPresenter.Update();
        }
    }
}