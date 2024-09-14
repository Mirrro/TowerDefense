using Gameplay.Blocks.Ground;
using Gameplay.Grid;
using UnityEngine;

namespace Gameplay.Blocks.Obstacle
{
    public class ObstacleBlockPresenter : IGridElement, IBuildModeBlock
    {
        private readonly ObstacleBlockView view;
        private readonly ObstacleBlockModel model;
        public bool IsSolid => true;
        public void OnGridPosition(Vector3 position)
        {
            model.Position = position;
            view.SetPosition(model.Position);
        }

        public ObstacleBlockPresenter(ObstacleBlockView view, ObstacleBlockModel model)
        {
            this.view = view;
            this.model = model;
        }

        public void Initialize()
        {
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
}