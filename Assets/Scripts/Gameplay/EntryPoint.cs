using System.Linq;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Vector2Int start;
    [SerializeField] private Vector2Int end;
    
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
    }

    private void Update()
    {
        var path = gridManager.GetPath(start, end);
        lineRenderer.positionCount = path.Count;
        lineRenderer.SetPositions(path.ToArray().Select(position => position + Vector3.up * .3f).ToArray());
    }
}