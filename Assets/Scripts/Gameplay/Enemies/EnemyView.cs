
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Enemies
{
    public class EnemyView : MonoBehaviour
    {
        public UnityEvent<Vector3> PositionChanged;
        public UnityEvent DestinationReached;

        [SerializeField] private float animatorUpdate = .3f;
        [SerializeField] private Animator animator;
        [SerializeField] private List<Renderer> renderers;
        [SerializeField] private GameObject freezeVisual;

        private Tween flashTween;
        private Tween deathTween;
        private Tween moveTween;

        private float lastUpdateTime;
        private bool isFreeze;

        public void MoveTo(Vector3 target)
        {
            if (isFreeze)
            {
                return;
            }
            moveTween?.Kill(false);
            moveTween = DOTween.Sequence()
                .Append(transform.DOMove(target, 1)
                    .SetEase(Ease.Linear)
                    .OnUpdate(() => PositionChanged.Invoke(transform.position))
                    .OnComplete(() =>
                    {
                        DestinationReached.Invoke();
                    }))
                .Join(transform.DORotate(Quaternion.LookRotation(target - transform.position, Vector3.up).eulerAngles, .3f));
        }

        public void StopMove()
        {
            moveTween?.Kill();
        }

        private void Start()
        {
            animator.enabled = false;
        }

        private void Update()
        {
            if (lastUpdateTime + animatorUpdate <= Time.time && !isFreeze)
            {
                Debug.Log("Update");
                lastUpdateTime = Time.time;
                animator.Update(animatorUpdate);
            }
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
            moveTween?.Kill(complete: false);
            foreach (var renderer in renderers)
            {
                renderer.material.DOFloat(1, "_Dissolve", 1);
            }
        }

        public void SetFreeze(bool isFreeze)
        {
            this.isFreeze = isFreeze;
            freezeVisual.SetActive(isFreeze);
        }
    }
}
