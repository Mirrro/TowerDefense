using UnityEngine.Pool;

namespace Gameplay.Enemies
{
    public class EnemyViewPool
    {
        public IObjectPool<EnemyView> MagePool;
        public IObjectPool<EnemyView> WarriorPool;

        public EnemyViewPool(EnemyView magePrefab, EnemyView warriorPrefab)
        {
            MagePool = new MonoBehaviourPool<EnemyView>(magePrefab);
            WarriorPool = new MonoBehaviourPool<EnemyView>(warriorPrefab);
        }

    }
}