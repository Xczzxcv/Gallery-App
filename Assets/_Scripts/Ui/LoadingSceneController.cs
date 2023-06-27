using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ui
{
internal class LoadingSceneController : MonoBehaviour
{
    [SerializeField]
    private Image progressBarImg;
    [SerializeField]
    private float minLoadTime;

    public void Load(AssetReference sceneToLoad, Action callback)
    {
        StartCoroutine(LoadingSceneUpdateCoroutine(sceneToLoad, callback));
    }

    private IEnumerator LoadingSceneUpdateCoroutine(AssetReference sceneToLoad, Action callback)
    {
        var loadSceneHandle = sceneToLoad.LoadSceneAsync(LoadSceneMode.Additive, false);
        const int completeAmount = 1;
        var maxDeltaPerSec = completeAmount / minLoadTime;
        float viewPercent = 0;
        while (viewPercent < completeAmount)
        {
            var currPercent = loadSceneHandle.PercentComplete;
            var maxDelta = maxDeltaPerSec * Time.deltaTime;
            viewPercent = Mathf.MoveTowards(viewPercent, currPercent, maxDelta);
            progressBarImg.fillAmount = viewPercent;
            yield return null;
        }

        var sceneInstance = loadSceneHandle.Result;
        yield return sceneInstance.ActivateAsync();

        SceneManager.SetActiveScene(sceneInstance.Scene);
        callback?.Invoke();
    }
}
}