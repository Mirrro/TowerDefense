using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class TowerView : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    public UnityEvent Hit;
    public Tween Fire(Transform[] targets)
    {
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        return bullet.transform.DOMove(targets[0].position, 1f)
            .OnComplete(() =>
            {
                Destroy(bullet);
                Hit?.Invoke();
            });
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}