using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Systems;
using Zenject;

namespace Gameplay.GameplayTasks
{
    public class BuildTowerBTask : IGameplayTask
    {
        [Inject] private TowerBuildSystem towerBuildSystem;
    
        public async UniTask Execute(CancellationToken cancellationToken)
        {
            await towerBuildSystem.BuildTower(Systems.Towers.TowerB, cancellationToken);
        }

        public class Factory : PlaceholderFactory<BuildTowerBTask>
        {
        }
    }
}