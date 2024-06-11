using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingSystem
{
    public event Action<IGridElement> ElementPlaced; 
    private const int towerCost = 100;
    
    private readonly GridManager gridManager;
    private readonly GridInteraction gridInteraction;

    private List<IGridElement> buildQueue = new List<IGridElement>();

    public BuildingSystem(GridManager gridManager, GridInteraction gridInteraction)
    {
        this.gridManager = gridManager;
        this.gridInteraction = gridInteraction;
    }

    public void Activate()
    {
        gridInteraction.OnGridCellSelected += HandleGridSelected;
    }

    public void Deactivate()
    {
        gridInteraction.OnGridCellSelected -= HandleGridSelected;
    }

    public void AddElementToBuildQueue(IGridElement gridElement)
    {
        buildQueue.Add(gridElement);
    }
    
    public void RemoveElementToBuildQueue(IGridElement gridElement)
    {
        buildQueue.Remove(gridElement);
    }

    private void HandleGridSelected(Vector2Int obj)
    {
        GridNode selectedGrid = gridManager.Grid.GridNodes[obj.x, obj.y];

        if (buildQueue.Count > 0)
        {
            IGridElement element = buildQueue.First();
            
            if (!selectedGrid.IsSolid)
            {
                selectedGrid.AddGirdElement(element);
                ElementPlaced?.Invoke(element);
            }
        }
    }
}