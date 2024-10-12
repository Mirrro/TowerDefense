using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private List<Renderer> renderers;

        private float lastUpdateTime;
        private Sequence flash;

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
            flash?.Kill();
            flash = DOTween.Sequence();
            foreach (var renderer in renderers)
            {
                flash.Join(renderer.material.DOColor(Color.red, .1f).SetLoops(2, LoopType.Yoyo));
            }
        }

        public void Dead()
        {
            animator.SetTrigger("isDead");
            foreach (var renderer in renderers)
            {
                renderer.material.DOFloat(1, "_Dissolve", 1);
            }
        }

        public List<Renderer> Renderers => renderers;

        public Bounds GetBounds()
        {
            var bounds = new Bounds();
            foreach (var renderer in renderers)
            {
                bounds.Encapsulate(renderer.bounds);
            }

            return bounds;
        }

        public void SetAnimationState(string stateName, bool isActive)
        {
            animator.SetBool(stateName, isActive);
        }
    }
}
