using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Towers.MVP
{
    public class TowerView : MonoBehaviour, ITowerView
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private AudioSource audioSource;
    
        private Tween placementTween;
        private Tween attackTween;
        private Transform target;

        public async void FireAtTarget(Transform target, Action callback)
        {
            this.target = target;
            attackTween?.Kill(true);
            attackTween = transform.DOJump(transform.position, .5f, 1, .5f);
            audioSource.pitch = Random.Range(.5f, 1.5f);
            audioSource.Play();
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
}