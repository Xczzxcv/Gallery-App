using System;
using UnityEngine.AddressableAssets;

namespace Infrastructure.GameStates
{
internal class LoadSceneGameState : IStateWithArgs<LoadSceneGameState.Args>
{
    public struct Args
    {
        public AssetReference SceneToLoad;
        public Action Callback;
    }

    private readonly ResourcesLocator _resourcesLocator;

    public LoadSceneGameState(ResourcesLocator resourcesLocator)
    {
        _resourcesLocator = resourcesLocator;
    }
    
    public async void Enter(Args args)
    {
        var loadingSceneHandle = _resourcesLocator.LoadingSceneAsset.LoadSceneAsync();
        await loadingSceneHandle.Task;
        var loadingSceneLocator = SceneHelper.GetRootComponent<LoadingSceneLocator>();
        loadingSceneLocator.LoadingController.Load(args.SceneToLoad, args.Callback);
    }

    public void Exit()
    {
        _resourcesLocator.LoadingSceneAsset.UnLoadScene();
    }
}
}