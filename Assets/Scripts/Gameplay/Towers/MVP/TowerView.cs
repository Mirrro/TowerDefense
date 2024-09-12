using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Gameplay.Towers.MVP
{
    public class TowerView : MonoBehaviour, ITowerView
    {
        [SerializeField] private LineRenderer lineRenderer;
    
        private Tween placementTween;
        private Tween attackTween;
        private Transform target;

        public async void FireAtTarget(Transform target, Action callback)
        {
            this.target = target;
            attackTween?.Kill(true);
            attackTween = transform.DOJump(transform.position, .5f, 1, .5f);
            await UniTask.WaitForSeconds(.5f);
            this.target = null;
            lineRenderer.positionCount = 0;
            callback?.Invoke();
        }

        private void Update()
        {
            if (target)
            {
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, transform.position + new Vector3(0,.8f,0));
                lineRenderer.SetPosition(1, target.position + new Vector3(0,.5f,0));
            }
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
            placementTween?.Kill(true);
            placementTween = transform.DOPunchScale(Vector3.one * .3f, .5f, 10);
        }
    }
}