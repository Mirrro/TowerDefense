using Gameplay.Towers.MVP;

namespace Gameplay.Towers.StateMachine
{
    public interface ITowerState
    {
        public void Initialize(TowerPresenter towerPresenter);
        public void Enter();
        public void Update();
        public void Exit();
    }
}