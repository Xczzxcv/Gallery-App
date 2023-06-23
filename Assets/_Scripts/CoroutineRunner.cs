using UnityEngine;

internal class CoroutineRunner : MonoBehaviour, ICoroutineRunner
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}