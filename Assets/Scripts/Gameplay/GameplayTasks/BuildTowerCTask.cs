using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Systems;
using Zenject;

namespace Gameplay.GameplayTasks
{
    public class BuildTowerCTask : IGameplayTask
    {
        [Inject] private TowerBuildSystem towerBuildSystem;
    
        public async UniTask Execute(CancellationToken cancellationToken)
        {
            await towerBuildSystem.BuildTower(Systems.Towers.TowerC, cancellationToken);
        }

        public class Factory : PlaceholderFactory<BuildTowerCTask>
        {
        }
    }
}