using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Screen = UnityEngine.Device.Screen;

namespace Infrastructure.GameStates
{
internal class ViewGameState : IStateWithArgs<ViewGameState.Args>
{
    public struct Args
    {
        public GalleryImage GalleryImage;
    }

    private readonly GameStateMachine _gameStateMachine;
    private readonly ResourcesLocator _resourcesLocator;
    private readonly IInputProvider _inputProvider;
    private SceneInstance _viewScene;
    private ViewSceneLocator _viewSceneLocator;
    private Scene _prevActiveScene;

    public ViewGameState(GameStateMachine gameStateMachine, 
        ResourcesLocator resourcesLocator,
        IInputProvider inputProvider)
    {
        _gameStateMachine = gameStateMachine;
        _resourcesLocator = resourcesLocator;
        _inputProvider = inputProvider;
    }

    public async void Enter(Args args)
    {
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;

        if (!_viewScene.Scene.isLoaded)
        {
            var viewSceneLoadHandle = _resourcesLocator.ViewSceneAsset.LoadSceneAsync(
                LoadSceneMode.Additive);
            _viewScene = await viewSceneLoadHandle.Task;
            
            _viewSceneLocator = SceneHelper.GetRootComponent<ViewSceneLocator>(_viewScene.Scene);
            _viewSceneLocator.GalleryImageView.ExitBtnClick += OnGalleryImageViewExitBtnClick;
        }
        
        _viewSceneLocator.GalleryImageView.Show(args.GalleryImage);
        _prevActiveScene = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(_viewScene.Scene);

        _inputProvider.BackBtnPress += OnBackBtnPress;
    }

    private void OnBackBtnPress()
    {
        GoBackToGallery();
    }

    private void OnGalleryImageViewExitBtnClick()
    {
        GoBackToGallery();
    }

    private void GoBackToGallery()
    {
        _gameStateMachine.EnterState<GalleryGameState>();
    }

    public void Exit()
    {
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;

        _inputProvider.BackBtnPress -= OnBackBtnPress;

        _viewSceneLocator.GalleryImageView.Hide();
        SceneManager.SetActiveScene(_prevActiveScene);
    }
}
}