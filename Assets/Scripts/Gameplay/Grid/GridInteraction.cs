using System;
using UnityEngine;
using Zenject;

public class GridInteraction : ITickable
{
    private readonly GridManager gridManager;
    private readonly MouseRayCast mouseRayCast;

    public event Action<Vector2Int> OnGridCellSelected;
    public event Action<Vector2Int> OnGridCellHovered;

    private Vector2Int hoveredCell;
    
    public GridInteraction(GridManager gridManager, MouseRayCast mouseRayCast)
    {
        this.gridManager = gridManager;
        this.mouseRayCast = mouseRayCast;
    }

    public void Tick()
    {
        if (mouseRayCast.TryGetPosition(out var hit))
        {
            var gridPos = gridManager.WorldToGridPosition(hit);

            if (!gridManager.IsInBound(gridPos))
            {
                return;
            }

            if (gridPos != hoveredCell)
            {
                hoveredCell = gridPos;
                OnGridCellHovered?.Invoke(hoveredCell);
            }

            if (Input.GetMouseButtonDown(0))
            {
                OnGridCellSelected?.Invoke(hoveredCell);
            }
        }
    }
}