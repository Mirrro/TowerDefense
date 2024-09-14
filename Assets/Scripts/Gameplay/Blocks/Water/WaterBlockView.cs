using DG.Tweening;
using Gameplay.Blocks.Ground;
using UnityEngine;

namespace Gameplay.Blocks.Water
{
    public class WaterBlockView : MonoBehaviour, IBuildModeBlock
    {
        [SerializeField] private ParticleSystem particleSystem;
        [SerializeField] private Renderer renderer;
        private Tween buildModeTween;
        
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void FireLava()
        {
            particleSystem.Play();   
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
