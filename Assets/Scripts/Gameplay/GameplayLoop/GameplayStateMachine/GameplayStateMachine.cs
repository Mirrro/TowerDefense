public class GameplayStateMachine
{
    private IGameplayState activeState;

    public void SwitchState(IGameplayState gameplayState)
    {
        activeState?.Deactivate();
        activeState = gameplayState;
        activeState?.Activate();
    }

    public void Update()
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