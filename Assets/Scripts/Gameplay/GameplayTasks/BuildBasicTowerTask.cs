using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

public class BuildBasicTowerTask : GameplayTask
{
    [Inject] private TowerBuildSystem towerBuildSystem;
    [Inject] private PlayerBank playerBank;
    
    protected override async UniTask OnExecute(CancellationToken cancellationToken)
    {
        await towerBuildSystem.BuildTower(Towers.TowerA, cancellationToken);
        playerBank.RemoveMoney(100);
    }

    protected override void OnCancel()
    {
    }
    
    public class Factory : PlaceholderFactory<BuildBasicTowerTask>
    {
    }
}