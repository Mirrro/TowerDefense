using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BridgeView : MonoBehaviour
{
    private Tween placementTween;
    
    public void PlaceOnPosition(Vector3 position)
    {
        placementTween?.Kill(true);
        transform.position = position;
        placementTween = transform.DOPunchScale(Vector3.one * .3f, .5f, 10);
    }
        
    public void HoverOnPosition(Vector3 position)
    {
        placementTween?.Kill();
        placementTween = transform.DOJump(position, .5f, 1, .3f);
    }
}
