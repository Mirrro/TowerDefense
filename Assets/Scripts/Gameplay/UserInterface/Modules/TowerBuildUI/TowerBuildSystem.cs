using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerBuildSystem
{
    private readonly UIManager uiManager;
    private readonly GridPlacementSystem gridPlacementSystem;
    private readonly PlayerBank playerBank;
    private readonly TowerBuilder towerBuilder;

    private List<TowerBuildData> towerBuildData;

    public TowerBuildSystem(UIManager uiManager, GridPlacementSystem gridPlacementSystem, PlayerBank playerBank, TowerBuilder towerBuilder)
    {
        this.uiManager = uiManager;
        this.gridPlacementSystem = gridPlacementSystem;
        this.playerBank = playerBank;
        this.towerBuilder = towerBuilder;
    }

    public void SetItems(List<TowerBuildData> data)
    {
        towerBuildData = data;
        uiManager.BuildMenuPresenter.SetItems(towerBuildData.Select(x => x.BuildMenuButtonData).ToList());
    }

    public void Activate()
    {
        uiManager.BuildMenuPresenter.ItemSelected += HandleItemSelected;
    }
    
    public void Deactivate()
    {
        uiManager.BuildMenuPresenter.ItemSelected -= HandleItemSelected;
    }

    private void HandleItemSelected(BuildMenuButtonData obj)
    {
        if (towerBuildData.Any(x => x.BuildMenuButtonData == obj))
        {
            TowerBuildData data = towerBuildData.First(x => x.BuildMenuButtonData == obj);

            if (playerBank.Coins >= data.Price)
            {
                IGridElement presenter;
                switch (data.Tower)
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
                
                gridPlacementSystem.PlaceElement(presenter, () => playerBank.RemoveMoney(data.Price));
                uiManager.BuildMenuPresenter.RemoveItem(data.BuildMenuButtonData);
            }
        }
    }
}

[Serializable]
public struct TowerBuildData
{
    public BuildMenuButtonData BuildMenuButtonData;
    public int Price;
    public Towers Tower;
}

public enum Towers
{
    TowerA,
    TowerB,
    TowerC
}