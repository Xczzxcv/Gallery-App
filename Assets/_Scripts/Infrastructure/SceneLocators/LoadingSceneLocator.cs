using Ui;
using UnityEngine;

namespace Infrastructure.SceneLocators
{
internal class LoadingSceneLocator : MonoBehaviour
{
    [field:SerializeField]
    public LoadingSceneController LoadingController { get; private set; }
}
}