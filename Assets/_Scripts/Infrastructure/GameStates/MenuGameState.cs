using UnityEngine.SceneManagement;

namespace Infrastructure.GameStates
{
internal class MenuGameState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly ResourcesLocator _resourcesLocator;

    private MenuSceneLocator _menuSceneLocator;

    public MenuGameState(GameStateMachine gameStateMachine, 
        ResourcesLocator resourcesLocator)
    {
        _resourcesLocator = resourcesLocator;
        _gameStateMachine = gameStateMachine;
    }

    public async void Enter()
    {
        var menuScene = await _resourcesLocator.MenuSceneAsset.LoadSceneAsync().Task;
        SceneManager.SetActiveScene(menuScene.Scene);

        _menuSceneLocator = SceneHelper.GetRootComponent<MenuSceneLocator>();
        _menuSceneLocator.GalleryBtn.onClick.AddListener(OnGalleryBtnClick);
    }

    private void OnGalleryBtnClick()
    {
        _gameStateMachine.EnterStateWithArgs<LoadSceneGameState, LoadSceneGameState.Args>(
            new LoadSceneGameState.Args
            {
                SceneToLoad = _resourcesLocator.GallerySceneAsset,
                Callback = OnGallerySceneLoad,
            }
        );
    }

    private void OnGallerySceneLoad()
    {
        _gameStateMachine.EnterState<GalleryGameState>();
    }

    public void Exit()
    {
        _menuSceneLocator.GalleryBtn.onClick.RemoveListener(OnGalleryBtnClick);
    }
}
}