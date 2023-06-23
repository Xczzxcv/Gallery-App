using Infrastructure.GameStates;
using UnityEngine;

internal class Bootstrap : MonoBehaviour
{
    [SerializeField]
    private ResourcesLocator resourcesLocator;

    [SerializeField]
    private CoroutineRunner coroutineRunner;
    
    private void Start()
    {
        var gameStateMachine = new GameStateMachine(resourcesLocator, coroutineRunner);
        gameStateMachine.EnterState<BootstrapGameState>();
    }
}