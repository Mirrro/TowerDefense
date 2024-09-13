using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Systems;
using Zenject;

namespace Gameplay.GameplayTasks
{
    public class BuildTowerATask : IGameplayTask
    {
        [Inject] private TowerBuildSystem towerBuildSystem;

        public async UniTask Execute(CancellationToken cancellationToken)
        {
            await towerBuildSystem.BuildTower(Systems.Towers.TowerA, cancellationToken);
        }
    
        public class Factory : PlaceholderFactory<BuildTowerATask>
        {
        }

   
    }
}