using Ext;
using Infrastructure.SceneLocators;

namespace Infrastructure.GameStates
{
internal class GalleryGameState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private GallerySceneLocator _gallerySceneLocator;

    private const string DATA_FOLDER_URL = "http://data.ikppbb.com/test-task-unity-data/pics/";
    private const int IMAGES_AMOUNT = 66;

    public GalleryGameState(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;
    }
    
    public void Enter()
    {
        if (_gallerySceneLocator is null)
        {
            _gallerySceneLocator = SceneHelper.GetRootComponent<GallerySceneLocator>();
            _gallerySceneLocator.GalleryUiController.Init(_gallerySceneLocator.GalleryImagesProvider,
                _gallerySceneLocator.GalleryImagesFactory, _gameStateMachine);
            
            for (int i = 0; i < IMAGES_AMOUNT; i++)
            {
                var imgUrl = $"{DATA_FOLDER_URL}{i + 1}.jpg";
                _gallerySceneLocator.GalleryUiController.AddImage(imgUrl);
            }
        }

        _gallerySceneLocator.GalleryUiController.Show();
    }

    public void Exit()
    {
        _gallerySceneLocator.GalleryUiController.Hide();
    }
}
}