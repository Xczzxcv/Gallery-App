using System;
using UnityEngine;
using UnityEngine.SceneManagement;

internal static class SceneHelper
{
    public static T GetRootComponent<T>() where T : Component
    {
        var rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        
        T resultComponent = null;
        foreach (var rootObject in rootObjects)
        {
            if (rootObject.TryGetComponent(out resultComponent))
            {
                break;
            }
        }

        if (!resultComponent)
        {
            throw new Exception($"Can't find {nameof(resultComponent)}");
        }

        return resultComponent;
    }
}