using UnityEngine;

internal interface IGalleryImagesProvider
{
    void GetImage(string imageUrl, ImageLoadedCallback imgCallback);
}

internal delegate void ImageLoadedCallback(Sprite sprite);