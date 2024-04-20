public class ObstacleBlockPresenter : IGridElement
{
    private readonly ObstacleBlockView view;
    private readonly ObstacleBlockModel model;
    public bool IsSolid => true;

    public ObstacleBlockPresenter(ObstacleBlockView view, ObstacleBlockModel model)
    {
        this.view = view;
        this.model = model;
    }

    public void Initialize()
    {
        view.SetPosition(model.Position);
    }
}