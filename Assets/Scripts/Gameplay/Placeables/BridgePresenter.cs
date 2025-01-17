using System;
using Gameplay.Grid;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

public class BridgePresenter : IPlaceable, IDisposable
{
    private readonly BridgeView view;
    private Vector3 position;
    
    public BridgePresenter(BridgeView view)
    {
        this.view = view;
    }
    
    public bool IsSolid => false;
    public void OnGridPosition(Vector3 position)
    {
        this.position = position;
        view.PlaceOnPosition(position);
    }

    public void HoverGridPosition(Vector3 position)
    {
        view.HoverOnPosition(position);
    }
    
    public class Factory : PlaceholderFactory<BridgeView, BridgePresenter>
    {
        
    }

    public void Dispose()
    {
        Object.Destroy(view.gameObject);
    }
}
