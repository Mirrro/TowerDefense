using Gameplay.Blocks.Ground;
using Gameplay.Blocks.Obstacle;
using Gameplay.Blocks.Water;
using Gameplay.Enemies;
using Gameplay.Towers.MVP;
using UnityEngine;

namespace Gameplay.Util
{
    [CreateAssetMenu(fileName = "ViewContainer", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
    public class ViewContainer : ScriptableObject
    {
        [SerializeField] private WaterBlockView waterBlock;
        public WaterBlockView WaterBlockView => waterBlock;
    
        [SerializeField] private GroundBlockView groundBlock;
        public GroundBlockView GroundBlockView => groundBlock;
    
        [SerializeField] private ObstacleBlockView obstacleBlock;
        public ObstacleBlockView ObstacleBlockView => obstacleBlock;
    
        [SerializeField] private EnemyView enemyView;
        public EnemyView EnemyView => enemyView;
    
        [SerializeField] private TowerView towerView;
        public TowerView TowerView => towerView;
    
        [SerializeField] private TowerView towerAView;
        public TowerView TowerAView => towerAView;
    
    }
}