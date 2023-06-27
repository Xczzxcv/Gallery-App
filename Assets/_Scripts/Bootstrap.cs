using Infrastructure.GameStates;
using UnityEngine;

internal class Bootstrap : MonoBehaviour
{
    [SerializeField]
    private ResourcesLocator resourcesLocator;
    [SerializeField]
    private InputController inputController;
    [SerializeField]
    private Transform common;
    
    private void Start()
    {
        DontDestroyOnLoad(common);

        var gameStateMachine = new GameStateMachine(resourcesLocator, inputController);
        gameStateMachine.EnterState<BootstrapGameState>();
    }
}