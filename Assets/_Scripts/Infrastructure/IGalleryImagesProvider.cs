﻿using UnityEngine;

namespace Infrastructure
{
internal interface IGalleryImagesProvider
{
    void GetImage(string imageUrl, ImageLoadedCallback imgCallback);
}

internal delegate void ImageLoadedCallback(Sprite sprite);
}