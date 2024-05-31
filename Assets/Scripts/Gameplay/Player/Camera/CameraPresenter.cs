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

    public void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleScroll();

        view.LerpTo(model.Position, model.Rotation, model.Damping);
        view.LerpCameraPosition(model.CameraPosition, model.Damping);
    }

    private void HandleMovement()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            direction += -view.transform.right;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += view.transform.right;
        }

        if (Input.GetKey(KeyCode.W))
        {
            direction += view.transform.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction += -view.transform.forward;
        }

        model.Position += direction * model.MoveSpeed * Time.deltaTime;
    }

    private void HandleRotation()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            model.Rotation *= Quaternion.Euler(0, model.RotationSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.E))
        {
            model.Rotation *= Quaternion.Euler(0, -model.RotationSpeed * Time.deltaTime, 0);
        }
    }

    private void HandleScroll()
    {
        float scroll = Input.mouseScrollDelta.y * model.ScrollSpeed * Time.deltaTime;
        Vector3 newCameraPosition = model.CameraPosition - new Vector3(0, scroll, -scroll * 0.5f);
        
        newCameraPosition.y = Mathf.Clamp(newCameraPosition.y, 0, 10);
        newCameraPosition.z = Mathf.Clamp(newCameraPosition.z, -10, -1);

        model.CameraPosition = newCameraPosition;
    }
}