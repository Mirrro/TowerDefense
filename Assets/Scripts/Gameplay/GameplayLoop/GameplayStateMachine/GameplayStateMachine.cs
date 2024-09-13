using Gameplay.GameplayLoop.GameplayStateMachine.GameplayStates;
using Zenject;

namespace Gameplay.GameplayLoop.GameplayStateMachine
{
    public class GameplayStateMachine : ITickable
    {
        private IGameplayState activeState;

        public void SwitchState(IGameplayState gameplayState)
        {
            activeState?.Deactivate();
            activeState = gameplayState;
            activeState?.Activate();
        }

        public void Tick()
        {
            if (activeState is {GameplayStateStatus: GameplayStateStatus.Running})
            {
                activeState?.Update();
            }
        }

        public void Pause()
        {
            activeState.OnPause();
            activeState.GameplayStateStatus = GameplayStateStatus.Paused;
        }

        public void Unpause()
        {
            activeState.OnUnpause();
            activeState.GameplayStateStatus = GameplayStateStatus.Running;
        }
    }
}