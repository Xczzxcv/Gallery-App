using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

internal class GalleryImagesProvider : MonoBehaviour, IGalleryImagesProvider
{
    private struct ImageLoadInfo
    {
        public UnityWebRequestAsyncOperation AsyncOperation;
        public List<ImageLoadedCallback> Callbacks;
    }

    private readonly Dictionary<string, Sprite> _images = new();
    private readonly Dictionary<string, ImageLoadInfo> _loadImagesInfos = new();

    public void GetImage(string imageUrl, ImageLoadedCallback imgCallback)
    {
        if (_images.TryGetValue(imageUrl, out var image))
        {
            imgCallback?.Invoke(image);
            return;
        }

        if (_loadImagesInfos.TryGetValue(imageUrl, out var imageLoadInfo))
        {
            imageLoadInfo.Callbacks.Add(imgCallback);
            return;
        }

        var webRequest = UnityWebRequestTexture.GetTexture(imageUrl);
        var webRequestOp = webRequest.SendWebRequest();
        _loadImagesInfos.Add(imageUrl, new ImageLoadInfo
        {
            AsyncOperation = webRequestOp,
            Callbacks = new List<ImageLoadedCallback> {imgCallback},
        });
    }

    private void Update()
    {
        var (imageUrl, imageLoadInfo)  = _loadImagesInfos.FirstOrDefault(pair => 
            pair.Value.AsyncOperation.webRequest.isDone);
        if (imageUrl is null)
        {
            return;
        }

        ProcessImgLoad(imageLoadInfo.AsyncOperation.webRequest, imageLoadInfo, imageUrl);
        _loadImagesInfos.Remove(imageUrl);
    }

    private void ProcessImgLoad(UnityWebRequest webRequest, ImageLoadInfo imageLoadInfo, string imageUrl)
    {
        var tex2d = new Texture2D(0, 0);
        tex2d.LoadImage(webRequest.downloadHandler.data);

        const int pixelsPerUnit = 1;
        const uint extrude = 0;
        var sprite = Sprite.Create(
            tex2d,
            new Rect(Vector2.zero, new Vector2(tex2d.width, tex2d.height)),
            new Vector2(.5f, .5f),
            pixelsPerUnit,
            extrude,
            SpriteMeshType.FullRect
        );
        _images.Add(imageUrl, sprite);

        foreach (var imgLoadedCallback in imageLoadInfo.Callbacks)
        {
            imgLoadedCallback?.Invoke(sprite);
        }
    }
}