using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.Grid;
using Gameplay.Towers;
using Gameplay.Towers.MVP;

namespace Gameplay.Systems
{
    public class TowerBuildSystem
    {
        private readonly GridPlacementSystem gridPlacementSystem;
        private readonly PlaceablesBuilder placeablesBuilder;
        private readonly GridManager gridManager;

        public TowerBuildSystem(GridPlacementSystem gridPlacementSystem, PlaceablesBuilder placeablesBuilder, GridManager gridManager)
        {
            this.gridPlacementSystem = gridPlacementSystem;
            this.placeablesBuilder = placeablesBuilder;
            this.gridManager = gridManager;
        }
        public async UniTask BuildTower(Towers towerType, CancellationToken cancellationToken)
        {
            TowerPresenter presenter;
            switch (towerType)
            {
                case Towers.TowerA:
                    presenter = placeablesBuilder.CreateElectricTower();
                    break;
                case Towers.TowerB:
                    presenter = placeablesBuilder.CreateFireTower();
                    break;
                case Towers.TowerC:
                    presenter = placeablesBuilder.CreateIceTower();
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

        public async UniTask BuildBridge(CancellationToken cancellationToken)
        {
            var presenter = placeablesBuilder.CreateBridge();
            var registration = cancellationToken.Register(() => OnCancel(presenter));
            gridManager.ActivateBuildModeVisual();
            await gridPlacementSystem.UserPlaceBridge(presenter, cancellationToken);
            gridManager.DeactivateBuildModeVisual();
            await registration.DisposeAsync();
        }

        private void OnCancel(IDisposable presenter)
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