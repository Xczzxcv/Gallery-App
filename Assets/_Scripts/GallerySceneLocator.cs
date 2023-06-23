using UnityEngine;

internal class GallerySceneLocator : MonoBehaviour
{
    [field: SerializeField]
    public GalleryUiController GalleryUiController { get; private set; }
    [field: SerializeField]
    public GalleryImagesProvider GalleryImagesProvider { get; private set; }
}