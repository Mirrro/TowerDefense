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
        public event Action EnemyDied;
        public event Action EnemyReachedGoal;
        public event Action FinalWaveSend;
        public Vector2Int StartPos => startPos;
        public Vector2Int EndPos => endPos;
    
        private readonly GridManager gridManager;
        private readonly EnemyBuilder enemyBuilder;
        public List<EnemyPresenter> ActiveEnemies => activeEnemies.Select(x => x.Presenter).ToList();
        private List<EnemyPresenterBuild> activeEnemies = new ();

        private Vector2Int startPos = new (0,0);
        private Vector2Int endPos = new (9, 5);

        private List<Wave> waves = new ()
        {
            new Wave(new List<EnemyTypes>()
            {
                EnemyTypes.Warrior,
                EnemyTypes.Mage,
                EnemyTypes.Mage,
                EnemyTypes.Mage,
                EnemyTypes.Mage,
            }),
            new Wave(new List<EnemyTypes>()
            {
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Mage,
            }),
            new Wave(new List<EnemyTypes>()
            {
                EnemyTypes.Mage,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Mage,
                EnemyTypes.Mage,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                 
                EnemyTypes.Warrior,
                EnemyTypes.Mage,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Mage,
                EnemyTypes.Warrior,
                EnemyTypes.Mage,
                EnemyTypes.Mage,
                EnemyTypes.Mage,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
            }),
            new Wave(new List<EnemyTypes>()
            {
                EnemyTypes.Mage,
                EnemyTypes.Mage,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Mage,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Mage,
                EnemyTypes.Mage,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Mage,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Mage,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Mage,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Mage,
                EnemyTypes.Mage,
                EnemyTypes.Mage,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Mage,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Mage,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Warrior,
                EnemyTypes.Mage,
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
                switch (enemyType)
                {
                    case EnemyTypes.Warrior:
                        SpawnWarriorEnemy(startPos);
                        break;
                    case EnemyTypes.Rouge:
                        break;
                    case EnemyTypes.Mage:
                        SpawnMageEnemy(startPos);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                await UniTask.WaitForSeconds(1f);
            }
            callback?.Invoke();
            currentWaveIndex++;
        
            if (currentWaveIndex == waves.Count)
            {
                FinalWaveSend?.Invoke();
            }
        }

        public void SpawnWarriorEnemy(Vector2Int startPos)
        {
            var enemy = enemyBuilder.CreateWarriorEnemy();
            enemy.Presenter.SetPosition(new Vector3(startPos.x, 0, startPos.y));
            enemy.Presenter.SetTarget(EndPos);
            enemy.Presenter.Died.AddListener(HandleDeath);
            activeEnemies.Add(enemy);
        }
        
        public void SpawnMageEnemy(Vector2Int startPos)
        {
            var enemy = enemyBuilder.CreateMageEnemy();
            enemy.Presenter.SetPosition(new Vector3(startPos.x, 0, startPos.y));
            enemy.Presenter.SetTarget(EndPos);
            enemy.Presenter.Died.AddListener(HandleDeath);
            activeEnemies.Add(enemy);
        }

        private void HandleDeath()
        {
            EnemyDied?.Invoke();
        }

        public IEnumerable<EnemyPresenter> FindEnemiesOnGrid(Vector3 position, int radius)
        {
            return ActiveEnemies.Where(enemy => IsInRadius(
                origin: gridManager.WorldToGridPosition(position),
                radius: radius,
                target: gridManager.WorldToGridPosition(enemy.Model.Position)));
        }

        private bool IsInRadius(Vector2Int origin, int radius, Vector2Int target)
        {
            int distance = Math.Max(Math.Abs(target.x - origin.x), Math.Abs(target.y - origin.y));
            return distance <= radius;
        }

        public void Tick()
        {
            foreach (var enemyPresenter in ActiveEnemies)
            {
                if (enemyPresenter is ITickable tickable)
                {
                    tickable.Tick();
                }
            }
        }

        public void Clear()
        {
            var cached = new List<EnemyPresenterBuild>(activeEnemies);
            activeEnemies.Clear();
            foreach (var enemyPresenterBuild in cached)
            {
                enemyPresenterBuild.Dispose();
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