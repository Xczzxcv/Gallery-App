using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.GameStates
{
internal class GameStateMachine : IStateMachine
{
    private readonly Dictionary<Type, IStateBase> _states = new ();
    private IStateBase _currentState;

    public GameStateMachine(ResourcesLocator resourcesLocator, ICoroutineRunner coroutineRunner)
    {
        AddState(new BootstrapGameState(this));
        AddState(new MenuGameState(this, resourcesLocator));
        AddState(new LoadSceneGameState());
        AddState(new GalleryGameState(this, coroutineRunner));
        AddState(new ViewGameState());
    }

    private void AddState(IStateBase state)
    {
        _states.Add(state.GetType(), state);
    }
    
    public void EnterState<TState>() where TState : IState
    {
        var nextState = _states[typeof(TState)] as IState;
        
        Debug.Log($"Exiting {_currentState?.GetType().Name}");
        _currentState?.Exit();
        _currentState = nextState;

        Debug.Log($"Entering {nextState.GetType().Name}");
        nextState.Enter();
    }

    public void EnterStateWithArgs<TState, TArgs>(TArgs args) 
        where TState : IStateWithArgs<TArgs> 
        where TArgs : struct
    {
        var nextState = _states[typeof(TState)] as IStateWithArgs<TArgs>;
        
        _currentState?.Exit();

        _currentState = nextState;
        nextState.Enter(args);
    }
}
}