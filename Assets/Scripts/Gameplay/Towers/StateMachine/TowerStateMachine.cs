using Gameplay.Towers.MVP;

namespace Gameplay.Towers.StateMachine
{
    public class TowerStateMachine
    {
        public ITowerState AttackState { get; }

        private ITowerState currentState;

        public TowerStateMachine(ITowerState attackState)
        {
            AttackState = attackState;
        }

        public void Initialize(TowerPresenter towerPresenter)
        {
            AttackState.Initialize(towerPresenter);
        }
    
        public void SetState(ITowerState nextState)
        {
            currentState?.Exit();
            currentState = nextState;
            currentState?.Enter();
        }

        public void Update()
        {
            currentState?.Update();
        }
    }
}