using System;

namespace Gameplay.GameplayLoop.GameplayStateMachine.GameplayStates
{
    public interface IGameplayState
    {
        public event Action StateComplete;
        public GameplayStateStatus GameplayStateStatus { get; set; }
        public void Activate();
        public void OnPause();
        public void OnUnpause();
        public void Update();
        public void Deactivate();
    }
}