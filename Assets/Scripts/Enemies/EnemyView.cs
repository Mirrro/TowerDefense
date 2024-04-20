using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class EnemyView : MonoBehaviour
{
    public UnityEvent<Vector3> PositionChanged;
    public UnityEvent DestinationReached;

    [SerializeField] private List<Renderer> renderers;

    private Tween flashTween;
    private Tween deathTween;
    private Tween moveTween;

    public void MoveTo(Vector3 target)
    {
        moveTween?.Kill(true);
        moveTween = DOTween.Sequence()
            .Append(transform.DOMove(target, 1)
                .SetEase(Ease.Linear)
                .OnUpdate(() => PositionChanged.Invoke(transform.position))
                .OnComplete(() =>
                {
                    DestinationReached.Invoke();
                    moveTween = null;
                }))
            .Join(transform.DORotate(Quaternion.LookRotation(target - transform.position, Vector3.up).eulerAngles, .3f));
    }

    public void UpdatePosition(Vector3 position)
    {
        transform.position = position;
        PositionChanged?.Invoke(position);
    }

    public void ReceiveDamage()
    {
        flashTween?.Kill(true);
        Sequence sequence = DOTween.Sequence();
        foreach (var renderer in renderers)
        {
            sequence.Join(renderer.material.DOColor(Color.red, .1f).SetLoops(2, LoopType.Yoyo));
        }

        flashTween = sequence;
    }

    public void Die()
    {
        moveTween.Kill(complete: false);
        foreach (var renderer in renderers)
        {
            renderer.material.DOFloat(1, "_Dissolve", 1);
        }
    }
}
