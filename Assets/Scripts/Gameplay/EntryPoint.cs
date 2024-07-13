using System.Linq;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Vector2Int start;
    [SerializeField] private Vector2Int end;

    private readonly LevelGenerator levelGenerator = new ();
    private GridManager gridManager;
    private LineRenderer lineRenderer;
    private CameraPresenter cameraPresenter;
    private EnemyManager enemyManager;
    private GridInteraction gridInteraction;
    private GameObject indicator;
    private TowerBuilder towerBuilder;
    private GridPlacementSystem gridPlacementSystem;
    private PlayerBank playerBank;
    private EnemyDeathRewardSystem enemyDeathRewardSystem;
    private UIManager uiManager;
    private GameplayLoop gameplayLoop;
    private TowerBuildSystem towerBuildSystem;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
        //gridManager = new GridManager(gridSize);
        //gridInteraction = new GridInteraction(gridManager, mouseRayCast);
        //gridPlacementSystem = new GridPlacementSystem(gridManager, gridInteraction);
        
        //cameraPresenter = new CameraPresenter(cameraView, new CameraModel());
        //enemyManager = new EnemyManager(gridManager);
        //towerFactory = new TowerFactory(enemyManager);
        //playerBank = new PlayerBank();
        //uiManager = new UIManager(uiViewReferences, playerBank);

        //towerBuildSystem = new TowerBuildSystem(uiManager, gridPlacementSystem, playerBank, towerFactory);
        //enemyDeathRewardSystem = new EnemyDeathRewardSystem(playerBank, enemyManager);
        
        // gameplayLoop = new GameplayLoop(
        //     new GameplayStateMachine(), 
        //     new PlayerTurnState(towerBuildSystem, uiManager, buildMenuItemsContainer),
        //     new EnemyTurnState(enemyDeathRewardSystem, enemyManager));
    }

    private void Update()
    {
        var path = gridManager.GetPath(start, end);
        lineRenderer.positionCount = path.Count;
        lineRenderer.SetPositions(path.ToArray().Select(position => position + Vector3.up * .3f).ToArray());
    }
}