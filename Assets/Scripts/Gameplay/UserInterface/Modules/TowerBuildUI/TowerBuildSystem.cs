using System;
using System.Threading;
using Cysharp.Threading.Tasks;

public class TowerBuildSystem
{
    private readonly GridPlacementSystem gridPlacementSystem;
    private readonly TowerBuilder towerBuilder;

    public TowerBuildSystem(GridPlacementSystem gridPlacementSystem, TowerBuilder towerBuilder)
    {
        this.gridPlacementSystem = gridPlacementSystem;
        this.towerBuilder = towerBuilder;
    }
    public async UniTask BuildTower(Towers towerType, CancellationToken cancellationToken)
    {
        TowerPresenter presenter;
        switch (towerType)
        {
            case Towers.TowerA:
                presenter = towerBuilder.CreateBasicTower();
                break;
            case Towers.TowerB:
                presenter = towerBuilder.CreateBasicTower();
                break;
            case Towers.TowerC:
                presenter = towerBuilder.CreateBasicTower();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        var registration = cancellationToken.Register(() => OnCancel(presenter));
        await gridPlacementSystem.UserPlaceElement(presenter, cancellationToken);
        await registration.DisposeAsync();
    }

    private void OnCancel(TowerPresenter presenter)
    {
        presenter.Dispose();
    }
}

public enum Towers
{
    TowerA,
    TowerB,
    TowerC
}