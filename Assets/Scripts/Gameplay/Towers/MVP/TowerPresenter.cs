using System;
using Gameplay.Grid;
using Gameplay.Towers.StateMachine;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Gameplay.Towers.MVP
{
    public class TowerPresenter : IPlaceable, ITickable, IDisposable
    {
        private readonly TowerModel model;
        private readonly TowerView view;
        private readonly TowerStateMachine towerStateMachine;

        public bool IsSolid => true;
    
        public TowerPresenter(TowerStateMachine towerStateMachine, TowerView view, TowerModel model)
        {
            this.towerStateMachine = towerStateMachine;
            this.model = model;
            this.view = view;
        }
    
        public void Initialize()
        {
            towerStateMachine.Initialize(this);
            ActivateBattleMode();
        }

        public int TowerRange => model.Range;

        public int TowerDamage => model.Damage;

        public float TowerFreezeTime => model.FreezeTime;
    
        public float TowerReloadTime => model.ReloadTime;

        public Vector3 TowerPosition => model.Position;

        public void ActivateBattleMode()
        {
            towerStateMachine.SetState(towerStateMachine.AttackState);
        }

        public void FireAtTarget(Transform target, Action callback)
        {
            view.FireAtTarget(target, callback);
        }

        public void OnGridPosition(Vector3 position)
        {
            model.Position = position;
            view.PlaceOnPosition(model.Position);
        }

        public void HoverGridPosition(Vector3 position)
        {
            view.HoverOnPosition(position);
        }

        public void Tick()
        {
            towerStateMachine.Update();
        }
    
        public void Dispose()
        {
            Object.Destroy(view.gameObject);
        }

        public class Factory : PlaceholderFactory<TowerStateMachine, TowerView, TowerModel, TowerPresenter>
        {
        
        }
    }
}
    