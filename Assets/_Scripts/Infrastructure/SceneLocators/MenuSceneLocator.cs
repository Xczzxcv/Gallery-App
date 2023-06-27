using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.SceneLocators
{
internal class MenuSceneLocator : MonoBehaviour
{
    [SerializeField]
    private Button galleryBtn;

    public Button GalleryBtn => galleryBtn;
}
}