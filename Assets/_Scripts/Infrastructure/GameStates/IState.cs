namespace Infrastructure.GameStates
{
internal interface IStateBase
{
    void Exit();

}

internal interface IState : IStateBase
{
    void Enter();
}

internal interface IStateWithArgs<TArgs> : IStateBase
    where TArgs : struct
{
    void Enter(TArgs args);
}
}
