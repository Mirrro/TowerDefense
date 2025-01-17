using System.Collections.Generic;
using Gameplay.Blocks.Ground;
using Gameplay.Blocks.Obstacle;
using Gameplay.Blocks.Water;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Gameplay.Util
{
    public class PresenterFactory : ITickable
    {
        private const string path = "ViewContainer";
        private List<ITickable> tickables = new List<ITickable>();

        private readonly WaterBlockPresenter.Factory waterBlockPresenterFactory;
        
        public PresenterFactory(WaterBlockPresenter.Factory waterBlockPresenterFactory)
        {
            this.waterBlockPresenterFactory = waterBlockPresenterFactory;
        }

        public WaterBlockPresenter CreateWaterBlockPresenter(WaterBlockModel model)
        {
            var container = Resources.Load<ViewContainer>(path);
            var presenter = waterBlockPresenterFactory.Create(Object.Instantiate(container.WaterBlockView, Vector3.zero, Quaternion.identity), model);
            tickables.Add(presenter);
            return presenter;
        }
    
        public GroundBlockPresenter CreateGroundBlockPresenter(GroundBlockModel model)
        {
            var container = Resources.Load<ViewContainer>(path);
            return new GroundBlockPresenter(Object.Instantiate(container.GroundBlockView, Vector3.zero, Quaternion.identity), model);
        }
    
        public ObstacleBlockPresenter CreateObstacleBlockPresenter(ObstacleBlockModel model)
        {
            var container = Resources.Load<ViewContainer>(path);
            return new ObstacleBlockPresenter(Object.Instantiate(container.ObstacleBlockView, Vector3.zero, Quaternion.identity), model);
        }

        public void Tick()
        {
            foreach (var tickable in tickables)
            {
                tickable.Tick();
            }
        }
    }
}