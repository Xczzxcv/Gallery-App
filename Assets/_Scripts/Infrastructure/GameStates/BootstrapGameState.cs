namespace Infrastructure.GameStates
{
internal class BootstrapGameState : IState
{
    private readonly GameStateMachine _gameStateMachine;

    public BootstrapGameState(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }
    
    public void Enter()
    {
        _gameStateMachine.EnterState<MenuGameState>();
    }

    public void Exit()
    { }
}
}