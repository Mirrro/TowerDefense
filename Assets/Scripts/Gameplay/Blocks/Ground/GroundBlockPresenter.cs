using Gameplay.Grid;
using UnityEngine;

namespace Gameplay.Blocks.Ground
{
    public class GroundBlockPresenter : IGridElement, IBuildModeBlock
    {
        private readonly GroundBlockView view;
        private readonly GroundBlockModel model;

        public GroundBlockPresenter(GroundBlockView view, GroundBlockModel model)
        {
            this.view = view;
            this.model = model;
        }

        public void Initialize()
        {
            view.SetPosition(model.Position);
        }

        public bool IsSolid => false;
        public void OnGridPosition(Vector3 position)
        {
            model.Position = position;
            view.SetPosition(model.Position);
        }

        public void EnterBuildMode(float duration)
        {
            view.EnterBuildMode(duration);
        }

        public void ExitBuildMode(float duration)
        {
            view.ExitBuildMode(duration);
        }
    }

    public interface IBuildModeBlock
    {
        public void EnterBuildMode(float duration);
        public void ExitBuildMode(float duration);
    }
}