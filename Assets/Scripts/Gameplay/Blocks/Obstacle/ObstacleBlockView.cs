using DG.Tweening;
using Gameplay.Blocks.Ground;
using UnityEngine;

namespace Gameplay.Blocks.Obstacle
{
    public class ObstacleBlockView : MonoBehaviour, IBuildModeBlock
    {
        [SerializeField] private Renderer renderer;
        private Tween buildModeTween;
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void EnterBuildMode(float duration)
        {
            buildModeTween?.Kill();
            buildModeTween = renderer.material.DOFloat(1, "_BuildingModeBlend", duration);
        }

        public void ExitBuildMode(float duration)
        {
            buildModeTween?.Kill();
            buildModeTween = renderer.material.DOFloat(0, "_BuildingModeBlend", duration);
        }
    }
}