using DG.Tweening;
using UnityEngine;

public class Movetest : MonoBehaviour
{
    void Start()
    {
        transform.DOMove(new Vector3(4,0,4), 2).SetLoops(-1);
    }
}
