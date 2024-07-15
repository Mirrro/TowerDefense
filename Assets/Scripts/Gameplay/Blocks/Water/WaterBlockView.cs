using UnityEngine;

public class WaterBlockView : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void FireLava()
    {
        particleSystem.Play();   
    }
}
