
using UnityEngine;

public class WaterBlockPresenter : IGridElement
{
    private readonly WaterBlockView view;
    private readonly WaterBlockModel model;
    public bool IsSolid => true;

    public void OnGridPosition(Vector3 position)
    {
        model.Position = position;
        view.SetPosition(model.Position);
    }
    
    public WaterBlockPresenter(WaterBlockView view, WaterBlockModel model)
    {
        this.view = view;
        this.model = model;
    }

    public void Initialize()
    {
        view.SetPosition(model.Position);
    }
}
