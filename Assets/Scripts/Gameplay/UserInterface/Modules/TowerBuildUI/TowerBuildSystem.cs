using System.Collections.Generic;
using UnityEngine;

public class TowerBuildSystem
{
    private readonly BuildMenuPresenter buildMenuPresenter;
    private readonly BuildingSystem buildingSystem;
    private readonly BuildMenuItemsContainer itemsContainer;
    private readonly PlayerBank playerBank;
    private readonly TowerFactory towerFactory;

    public TowerBuildSystem(BuildMenuPresenter buildMenuPresenter, BuildingSystem buildingSystem, BuildMenuItemsContainer itemsContainer)
    {
        this.buildMenuPresenter = buildMenuPresenter;
        this.buildingSystem = buildingSystem;
        this.itemsContainer = itemsContainer;
    }

    public void Activate()
    {
        buildMenuPresenter.SetItems(new List<BuildMenuButtonData>(){ new BuildMenuButtonData(){}});
        buildMenuPresenter.ItemSelected += HandleItemSelected;
    }

    private void HandleItemSelected(BuildMenuButtonData obj)
    {
        // Build
        TowerPresenter presenter =
            towerFactory.CreateBasicTower(new TowerModel(new Vector3(obj.x, 0, obj.y), 10, 10, 1));
        
            && playerBank.Coins >= towerCost
            
        playerBank.RemoveMoney(towerCost);
    }

    public void Deactivate()
    {
        buildMenuPresenter.ItemSelected -= HandleItemSelected;
    }
}

public class BuildMenuItemsContainer : MonoBehaviour
{
    public List<BuildMenuButtonData> Items = new List<BuildMenuButtonData>();
}

public struct TowerBuildData
{
    private BuildMenuButtonData buildMenuButtonData;
}

public enum