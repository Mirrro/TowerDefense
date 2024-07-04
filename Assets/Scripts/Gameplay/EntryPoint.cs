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
    [SerializeField] private BuildMenuItemsContainer buildMenuItemsContainer;
    
    private readonly LevelGenerator levelGenerator = new ();
    private readonly MouseRayCast mouseRayCast = new ();
    private GridManager gridManager;
    private LineRenderer lineRenderer;
    private CameraPresenter cameraPresenter;
    private EnemyManager enemyManager;
    private GridInteraction gridInteraction;
    private GameObject indicator;
    private TowerFactory towerFactory;
    private GridPlacementSystem gridPlacementSystem;
    private PlayerBank playerBank;
    private EnemyDeathRewardSystem enemyDeathRewardSystem;
    private UIManager uiManager;
    private GameplayLoop gameplayLoop;
    private TowerBuildSystem towerBuildSystem;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
        gridManager = new GridManager(gridSize);
        gridInteraction = new GridInteraction(gridManager, mouseRayCast);
        gridPlacementSystem = new GridPlacementSystem(gridManager, gridInteraction);
        
        cameraPresenter = new CameraPresenter(cameraView, new CameraModel());
        enemyManager = new EnemyManager(gridManager);
        towerFactory = new TowerFactory(enemyManager);
        playerBank = new PlayerBank();
        uiManager = new UIManager(uiViewReferences, playerBank);

        towerBuildSystem = new TowerBuildSystem(uiManager, gridPlacementSystem, playerBank, towerFactory);
        enemyDeathRewardSystem = new EnemyDeathRewardSystem(playerBank, enemyManager);
        
        gameplayLoop = new GameplayLoop(
            new GameplayStateMachine(), 
            new PlayerTurnState(towerBuildSystem, uiManager, buildMenuItemsContainer),
            new EnemyTurnState(enemyDeathRewardSystem, enemyManager));
    }

    private void Start()
    {
        levelGenerator.PopulateGrid(gridManager.Grid);
        playerBank.AddMoney(1000);
        uiManager.Initialize();
        gameplayLoop.Start();
    }

    private void Update()
    {
        cameraPresenter.Update();
        gameplayLoop.Update();
        var path = gridManager.GetPath(start, end);
        lineRenderer.positionCount = path.Count;
        lineRenderer.SetPositions(path.ToArray().Select(position => position + Vector3.up * .3f).ToArray());
        
        gridInteraction.Update();
        towerFactory.Update();
    }
}