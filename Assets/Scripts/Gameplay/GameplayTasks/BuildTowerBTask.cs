using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

public class BuildTowerBTask : IGameplayTask
{
    [Inject] private TowerBuildSystem towerBuildSystem;
    
    public async UniTask Execute(CancellationToken cancellationToken)
    {
        await towerBuildSystem.BuildTower(Towers.TowerB, cancellationToken);
    }

    public class Factory : PlaceholderFactory<BuildTowerBTask>
    {
    }
}