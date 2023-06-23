using System;
using UnityEditor;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

[Serializable]
public class SceneAssetReference : AssetReferenceT<SceneAsset>
{
    public SceneAssetReference(string guid) : base(guid)
    { }
}

namespace Infrastructure.GameStates
{
internal class LoadSceneGameState : IStateWithArgs<LoadSceneGameState.Args>
{
    public struct Args
    {
        public SceneAssetReference SceneToLoad;
        public Action Callback;
    }
    
    public async void Enter(Args args)
    {
        var currentScene = SceneManager.GetActiveScene();
        var sceneInstance = await args.SceneToLoad.LoadSceneAsync().Task;
        args.Callback?.Invoke();
        SceneManager.SetActiveScene(sceneInstance.Scene);
    }

    public void Exit()
    { }
}
}