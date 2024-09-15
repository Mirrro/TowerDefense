using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Grid;
using Gameplay.Towers.MVP;
using Gameplay.Util;

namespace Gameplay.Systems
{
    public class TowerBuildSystem
    {
        private readonly GridPlacementSystem gridPlacementSystem;
        private readonly TowerBuilder towerBuilder;
        private readonly GridManager gridManager;

        public TowerBuildSystem(GridPlacementSystem gridPlacementSystem, TowerBuilder towerBuilder, GridManager gridManager)
        {
            this.gridPlacementSystem = gridPlacementSystem;
            this.towerBuilder = towerBuilder;
            this.gridManager = gridManager;
        }
        public async UniTask BuildTower(Towers towerType, CancellationToken cancellationToken)
        {
            TowerPresenter presenter;
            switch (towerType)
            {
                case Towers.TowerA:
                    presenter = towerBuilder.CreateElectricTower();
                    break;
                case Towers.TowerB:
                    presenter = towerBuilder.CreateFireTower();
                    break;
                case Towers.TowerC:
                    presenter = towerBuilder.CreateIceTower();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var registration = cancellationToken.Register(() => OnCancel(presenter));
            gridManager.ActivateBuildModeVisual();
            await gridPlacementSystem.UserPlaceElement(presenter, cancellationToken);
            gridManager.DeactivateBuildModeVisual();
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
}