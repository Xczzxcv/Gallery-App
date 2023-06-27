using Ui;
using UnityEngine;

namespace Infrastructure
{
internal class GalleryImagesFactory : MonoBehaviour, IGalleryImagesFactory
{
    [SerializeField]
    private GalleryImage galleryImagePrefab;

    public GalleryImage Create(string imageUrl, Transform parentObject)
    {
        var galleryImage = Instantiate(galleryImagePrefab, parentObject);
        galleryImage.Setup(imageUrl);

        return galleryImage;
    }
}
}