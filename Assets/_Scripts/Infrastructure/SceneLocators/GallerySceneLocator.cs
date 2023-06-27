using Ui;
using UnityEngine;

namespace Infrastructure.SceneLocators
{
internal class GallerySceneLocator : MonoBehaviour
{
    [field: SerializeField]
    public GalleryUiController GalleryUiController { get; private set; }
    [field: SerializeField]
    public GalleryImagesProvider GalleryImagesProvider { get; private set; }
    [field: SerializeField]
    public GalleryImagesFactory GalleryImagesFactory { get; private set; }
}
}