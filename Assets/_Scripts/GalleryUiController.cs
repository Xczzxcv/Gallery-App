using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal class GalleryUiController : UIBehaviour
{
    [SerializeField]
    private GalleryImage galleryImagePrefab;

    [SerializeField]
    private RectTransform parentObject;

    [SerializeField]
    private GridLayoutGroup grid;

    [SerializeField]
    private ScrollRect scrollRect;

    [SerializeField]
    private bool width;

    [field: SerializeField]
    private float CellSize { get; set; }

    private readonly List<GalleryImage> _images = new();
    private readonly Vector3[] _fourCornersArray = new Vector3[4];
    private GalleryImagesProvider _galleryImagesProvider;
    private bool _needUpdate;

    protected override void Awake()
    {
        scrollRect.onValueChanged.AddListener(OnScrollChanged);
        UpdateCellSize();
    }

    public void Init(GalleryImagesProvider galleryImagesProvider)
    {
        _galleryImagesProvider = galleryImagesProvider;
    }

    private void OnScrollChanged(Vector2 arg0)
    {
        _needUpdate = true;
    }

    private void LateUpdate()
    {
        if (_needUpdate)
        {
            UpdateCellsVisibility();
            _needUpdate = false;
        }
    }

    private void UpdateCellsVisibility()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(parentObject);
        foreach (var galleryImage in _images)
        {
            ProcessImage(galleryImage);
        }
    }

    private void ProcessImage(GalleryImage galleryImage)
    {
        if (galleryImage.Seen || !IsVisible(galleryImage))
        {
            return;
        }

        galleryImage.MarkSeen();
        _galleryImagesProvider.GetImage(galleryImage.ImageUrl, galleryImage.SetImg);
        Debug.Log($"Seen img {galleryImage.ImageUrl}");
    }

    protected override void OnRectTransformDimensionsChange()
    {
        UpdateCellSize();
    }

    private bool IsVisible(GalleryImage galleryImage)
    {
        galleryImage.RectTransform.GetWorldCorners(_fourCornersArray);
        var isVisible1 = RectTransformUtility.RectangleContainsScreenPoint(scrollRect.viewport, _fourCornersArray[0]);
        var isVisible2 = RectTransformUtility.RectangleContainsScreenPoint(scrollRect.viewport, _fourCornersArray[2]);
        return isVisible1 || isVisible2;
    }

    private void UpdateCellSize()
    {
        var cellSize = width
            ? Screen.width * CellSize
            : Screen.height * CellSize;
        grid.cellSize = Vector2.one * cellSize;
    }

    public void AddImage(string imageUrl)
    {
        var galleryImage = Instantiate(galleryImagePrefab, parentObject);
        galleryImage.Setup(imageUrl);
        _images.Add(galleryImage);
        _needUpdate = true;
    }
}