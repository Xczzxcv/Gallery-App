namespace Infrastructure.GameStates
{
internal class GalleryGameState : IState
{
    private readonly GameStateMachine _gameStateMachine;
    private readonly ICoroutineRunner _coroutineRunner;

    private const string DATA_FOLDER_URL = "http://data.ikppbb.com/test-task-unity-data/pics/";
    private const int IMAGES_AMOUNT = 66;

    public GalleryGameState(GameStateMachine gameStateMachine,
        ICoroutineRunner coroutineRunner)
    {
        _gameStateMachine = gameStateMachine;
        _coroutineRunner = coroutineRunner;
    }
    
    public void Enter()
    {
        var gallerySceneLocator = SceneHelper.GetRootComponent<GallerySceneLocator>();
        gallerySceneLocator.GalleryUiController.Init(gallerySceneLocator.GalleryImagesProvider);
        for (int i = 0; i < IMAGES_AMOUNT; i++)
        {
            var imgUrl = $"{DATA_FOLDER_URL}{i + 1}.jpg";
            gallerySceneLocator.GalleryUiController.AddImage(imgUrl);
        }
    }

    public void Exit()
    { }
}
}