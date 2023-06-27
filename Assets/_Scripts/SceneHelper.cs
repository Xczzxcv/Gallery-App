using System;
using UnityEngine;
using UnityEngine.SceneManagement;

internal static class SceneHelper
{
    public static T GetRootComponent<T>(Scene scene = default) where T : Component
    {
        if (scene == default)
        {
            scene = SceneManager.GetActiveScene();
        }

        var rootObjects = scene.GetRootGameObjects();

        foreach (var rootObject in rootObjects)
        {
            if (rootObject.TryGetComponent(out T resultComponent))
            {
                return resultComponent;
            }
        }

        throw new Exception($"Can't find {nameof(T)}");
    }
}