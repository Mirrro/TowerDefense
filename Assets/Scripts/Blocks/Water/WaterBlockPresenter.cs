public class WaterBlockPresenter : IGridElement
{
    private readonly WaterBlockView view;
    private readonly WaterBlockModel model;
    public bool IsSolid => true;

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
