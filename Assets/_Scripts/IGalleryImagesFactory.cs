using UnityEngine;

internal interface IGalleryImagesFactory
{
    GalleryImage Create(string imageUrl, Transform parentObject);
}