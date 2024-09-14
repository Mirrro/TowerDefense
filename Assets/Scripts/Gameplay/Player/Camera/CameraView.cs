using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Sequence = DG.Tweening.Sequence;

namespace Gameplay.Player.Camera
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera camera;
        public UnityEvent<Vector3> ChangedPosition = new ();

        private Sequence parentTween;
        private Tween cameraTween;
    
        public void LerpTo(Vector3 position, Quaternion rotation, float speed)
        {
            parentTween?.Kill();
            parentTween = DOTween.Sequence();
            parentTween.Join(transform.DOMove(position, 1 / speed)
                .OnUpdate(() => ChangedPosition?.Invoke(transform.position)));
            parentTween.Join(transform.DORotateQuaternion(rotation, speed));
        }

        public void LerpCameraPosition(Vector3 position, float speed)
        {
            cameraTween?.Kill();
            cameraTween = camera.transform.DOLocalMove(position, speed);
        }

        private void Update()
        {
            camera.transform.LookAt(transform);
        }
    }
}
