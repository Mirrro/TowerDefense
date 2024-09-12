using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

public class BuildTowerATask : IGameplayTask
{
    [Inject] private TowerBuildSystem towerBuildSystem;

    public async UniTask Execute(CancellationToken cancellationToken)
    {
        await towerBuildSystem.BuildTower(Towers.TowerA, cancellationToken);
    }
    
    public class Factory : PlaceholderFactory<BuildTowerATask>
    {
    }

   
}