using Ui;
using UnityEngine;

namespace Infrastructure
{
internal interface IGalleryImagesFactory
{
    GalleryImage Create(string imageUrl, Transform parentObject);
}
}