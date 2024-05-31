using UnityEngine;

public class CameraModel
{
    public Vector3 Position = Vector3.zero;
    public Vector3 CameraPosition = Vector3.back * 3;
    public Quaternion Rotation = Quaternion.identity;
    public float MoveSpeed = 10f;
    public float RotationSpeed = 90f;
    public float ScrollSpeed = 100f;
    public float Damping = 1f;
}
