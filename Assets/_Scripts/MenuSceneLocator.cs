using UnityEngine;
using UnityEngine.UI;

internal class MenuSceneLocator : MonoBehaviour
{
    [SerializeField]
    private Button galleryBtn;

    public Button GalleryBtn => galleryBtn;
}