using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Gameplay.Grid;
using Gameplay.Util;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemies
{
    public class EnemyManager : ITickable
    {
        public event Action<IEnemyPresenter> EnemyDied;
        public event Action<IEnemyPresenter> EnemyReachedGoal;
        public event Action FinalWaveSend;
        public Vector2Int StartPos => startPos;
        public Vector2Int EndPos => endPos;
    
        private readonly GridManager gridManager;
        private readonly EnemyBuilder enemyBuilder;
        public int ActiveEnemiesCount => activeEnemies.Count;
        private List<IEnemyPresenter> activeEnemies = new ();

        private Vector2Int startPos = new (0,0);
        private Vector2Int endPos = new (29, 7);

        private List<Wave> waves = new ()
        {
            new Wave(new List<EnemyTypes>()
            {
                EnemyTypes.Warrior,
            }),
            new Wave(new List<EnemyTypes>()
            {
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
            }),
            new Wave(new List<EnemyTypes>()
            {
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
            }),
            new Wave(new List<EnemyTypes>()
            {
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
            }),
        };
        private int currentWaveIndex = 0;

        public EnemyManager(GridManager gridManager, EnemyBuilder enemyBuilder)
        {
            this.gridManager = gridManager;
            this.enemyBuilder = enemyBuilder;
        }

        public async UniTask SendNextWave(Action callback = null)
        {
            if (currentWaveIndex >= waves.Count)
            {
                return;
            }

            foreach (var enemyType in waves[currentWaveIndex].Enemies)
            {
                SpawnEnemy(startPos);
                await UniTask.WaitForSeconds(1f);
            }
            callback?.Invoke();
            currentWaveIndex++;
        
            if (currentWaveIndex == waves.Count)
            {
                FinalWaveSend?.Invoke();
            }
        }

        public void SpawnEnemy(Vector2Int startPos)
        {
            var enemy = enemyBuilder.CreateBasicEnemy();
            enemy.SetPosition(new Vector3(startPos.x, 0, startPos.y));
            enemy.SetTarget(EndPos);
            enemy.Died.AddListener(() => HandleDeath(enemy));
            enemy.ReachedGoal.AddListener(() => HandleEnemyReachedGoal(enemy));
            activeEnemies.Add(enemy);
        }

        private void HandleDeath(IEnemyPresenter enemyPresenter)
        {
            activeEnemies.Remove(enemyPresenter);
            EnemyDied?.Invoke(enemyPresenter);
        }

        private void HandleEnemyReachedGoal(IEnemyPresenter enemy)
        {
            EnemyReachedGoal?.Invoke(enemy);
            activeEnemies.Remove(enemy);
        }

        public IEnumerable<IEnemyPresenter> FindEnemiesOnGrid(Vector3 position, int radius)
        {
            return activeEnemies.Where(enemy => IsInRadius(
                origin: gridManager.WorldToGridPosition(position),
                radius: radius,
                target: gridManager.WorldToGridPosition(enemy.GetPosition())));
        }

        private bool IsInRadius(Vector2Int origin, int radius, Vector2Int target)
        {
            int distance = Math.Max(Math.Abs(target.x - origin.x), Math.Abs(target.y - origin.y));
            return distance <= radius;
        }

        public void Tick()
        {
            foreach (var enemyPresenter in activeEnemies)
            {
                if (enemyPresenter is ITickable tickable)
                {
                    tickable.Tick();
                }
            }
        }
    }

    public enum EnemyTypes
    {
        Warrior,
        Rouge,
        Mage
    }

    public class Wave
    {
        public List<EnemyTypes> Enemies;

        public Wave(List<EnemyTypes> enemies)
        {
            Enemies = enemies;
        }
    }
}