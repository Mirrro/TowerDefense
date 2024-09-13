using Gameplay.Grid;
using UnityEngine;

namespace Gameplay.Blocks.Ground
{
    public class GroundBlockPresenter : IGridElement
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
    }
}