using UnityEngine;

public class CameraPresenter
{
    private readonly CameraView view;
    private readonly CameraModel model;

    public CameraPresenter(CameraView view, CameraModel model)
    {
        this.view = view;
        this.model = model;
    }

    public void Initialize()
    {
        view.PressedLeft.AddListener(HandlePressedLeft);
        view.PressedRight.AddListener(HandlePressedRight);
        view.ChangedPosition.AddListener(HandlePositionChange);
    }

    private void HandlePressedRight()
    {
        view.LerpTo(model.Position + Vector3.right, model.Speed);
    }

    private void HandlePositionChange(Vector3 arg0)
    {
        model.Position = arg0;
    }

    private void HandlePressedLeft()
    {
        view.LerpTo(model.Position + Vector3.left, model.Speed);
    }
}
