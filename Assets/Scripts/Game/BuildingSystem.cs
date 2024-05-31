using System;
using UnityEngine;

public class BuildingSystem
{
    public event Action<TowerPresenter> TowerBuild; 
    private const int towerCost = 100;
    
    private readonly GridManager gridManager;
    private readonly GridInteraction gridInteraction;
    private readonly PlayerBank playerBank;
    private readonly TowerFactory towerFactory;

    public BuildingSystem(GridManager gridManager, GridInteraction gridInteraction, PlayerBank playerBank, TowerFactory towerFactory)
    {
        this.gridManager = gridManager;
        this.gridInteraction = gridInteraction;
        this.playerBank = playerBank;
        this.towerFactory = towerFactory;
    }

    public void Activate()
    {
        gridInteraction.OnGridCellSelected += HandleGridSelected;
    }

    public void Deactivate()
    {
        gridInteraction.OnGridCellSelected -= HandleGridSelected;
    }

    private void HandleGridSelected(Vector2Int obj)
    {
        GridNode selectedGrid = gridManager.Grid.GridNodes[obj.x, obj.y];
        
        if (!selectedGrid.IsSolid && playerBank.Coins >= towerCost)
        {
            TowerPresenter presenter =
                towerFactory.CreateTower(new TowerModel(new Vector3(obj.x, 0, obj.y), 10, 10, 1));
            selectedGrid.AddGirdElement(presenter);
            playerBank.RemoveMoney(towerCost);
            TowerBuild?.Invoke(presenter);
        }
    }
}