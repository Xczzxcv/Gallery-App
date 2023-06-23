namespace Infrastructure.GameStates
{
internal interface IStateMachine
{
    void EnterState<TState>() where TState : IState;
    void EnterStateWithArgs<TState, TArgs>(TArgs args) where TState : IStateWithArgs<TArgs> 
        where TArgs : struct;
}
}