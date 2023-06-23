using System.Collections;
using UnityEngine;

internal interface ICoroutineRunner
{
    Coroutine StartCoroutine(IEnumerator coroutine);
}