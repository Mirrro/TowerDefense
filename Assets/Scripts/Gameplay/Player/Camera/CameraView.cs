using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class CameraView : MonoBehaviour
{
    public UnityEvent PressedLeft = new ();
    public UnityEvent PressedRight = new ();
    public UnityEvent<Vector3> ChangedPosition = new ();
    private Tween moveTween;
    public void LerpTo(Vector3 position, float speed)
    {
        Debug.Log($"Move to {position}");
        moveTween?.Kill();
        moveTween = transform.DOMove(position, 1 / speed)
            .OnUpdate(() => ChangedPosition?.Invoke(transform.position));
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            PressedLeft?.Invoke();
            Debug.Log("Pressed Left");
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            PressedRight?.Invoke();
            Debug.Log("Pressed Right");
        }
    }
}
