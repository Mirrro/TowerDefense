using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Enemies
{
    public class EnemyView : MonoBehaviour, IPoolable, IEnemyView
    {
        [SerializeField] private Animator animator;
        [SerializeField] private List<Renderer> renderers;
        [SerializeField] private Healthbar healthbar;
        [SerializeField] private Transform targetPoint;
        
        private Sequence flash;
        private Sequence dissolve;

        public UnityEvent mouseDown = new UnityEvent();
        public Transform TargetPoint => targetPoint ?? transform;
        public Transform Transform => transform;
        public UnityEvent MouseDown => mouseDown;

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }

        public void Flash()
        {
            flash?.Kill(complete: true);
            flash = DOTween.Sequence();
            foreach (var renderer in renderers)
            {
                flash.Join(renderer.material.DOColor(Color.red, .1f).SetLoops(2, LoopType.Yoyo));
            }

            flash.Join(transform.DOPunchScale(Vector3.one * .1f, .1f, 2));
        }

        public void Spawn()
        {
            animator.SetTrigger("Spawn");
        }

        public void SetWalk(bool isWalking)
        {
            animator.SetBool("isWalking", isWalking);
        }

        public void SetAnimatorSpeed(float speed)
        {
            animator.speed = speed;
        }

        public void SetDead(bool isDead)
        {
            animator.SetBool("isDead", isDead);
            dissolve?.Kill(complete: true);
            dissolve = DOTween.Sequence();
            foreach (var renderer in renderers)
            {
                dissolve.Join(renderer.material.DOFloat(1, "_Dissolve", 1).OnComplete(() => gameObject.SetActive(false)));
            }
        }

        public void SetHealthBarFill(float percentage)
        {
            healthbar.SetHealth(percentage);
        }

        public void OnReleased()
        {
            flash?.Kill(complete: true);
            dissolve?.Kill(complete: true);

            animator.ResetTrigger("Spawn");
            animator.SetBool("isWalking", false);
            animator.SetBool("isDead", false);
            animator.speed = 1;
            
            foreach (var renderer in renderers)
            {
                renderer.material.SetFloat("_Dissolve", 0);
                renderer.material.color = Color.white;
            }
            
            healthbar.SetHealth(1);
        }

        private void OnMouseDown()
        {
            MouseDown?.Invoke();
        }
    }
}
