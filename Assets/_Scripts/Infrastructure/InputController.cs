using System;
using UnityEngine;

namespace Infrastructure
{
internal class InputController : MonoBehaviour, IInputProvider
{
    public event Action BackBtnPress;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackBtnPress?.Invoke();
        }
    }
}
}