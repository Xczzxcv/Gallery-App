using Ui;
using UnityEngine;

namespace Infrastructure.SceneLocators
{
internal class ViewSceneLocator : MonoBehaviour
{
    [field:SerializeField]
    public GalleryImageView GalleryImageView { get; private set; }
}
}