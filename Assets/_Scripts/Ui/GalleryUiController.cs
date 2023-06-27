using System.Collections.Generic;
using Infrastructure;
using Infrastructure.GameStates;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Ui
{
internal class GalleryUiController : UIBehaviour
{
    [SerializeField]
    private RectTransform imagesParentObject;
    [SerializeField]
    private GridLayoutGroup grid;
    [SerializeField]
    private ScrollRect scrollRect;
    [SerializeField]
    private RectTransform rootObject;
    [SerializeField]
    private bool width;
    [SerializeField]
    private float cellSizeCft;

    private readonly List<GalleryImage> _images = new();
    private readonly Vector3[] _fourCornersArray = new Vector3[4];
    private IGalleryImagesProvider _galleryImagesProvider;
    private IGalleryImagesFactory _galleryImagesFactory;
    private bool _needUpdate;
    private GameStateMachine _gameStateMachine;

    protected override void Awake()
    {
        scrollRect.onValueChanged.AddListener(OnScrollChanged);
        UpdateCellSize();
    }

    private void LateUpdate()
    {
        if (_needUpdate)
        {
            UpdateCellsVisibility();
            _needUpdate = false;
        }
    }

    public void Init(IGalleryImagesProvider galleryImagesProvider,
        IGalleryImagesFactory galleryImagesFactory,
        GameStateMachine gameStateMachine)
    {
        _galleryImagesProvider = galleryImagesProvider;
        _galleryImagesFactory = galleryImagesFactory;
        _gameStateMachine = gameStateMachine;
    }

    public void Show()
    {
        rootObject.gameObject.SetActive(true);
    }

    public void Hide()
    {
        rootObject.gameObject.SetActive(false);
    }

    public void AddImage(string imageUrl)
    {
        var galleryImage = _galleryImagesFactory.Create(imageUrl, imagesParentObject);
        galleryImage.ImgClick += OnGalleryImageClick;
        _images.Add(galleryImage);
        _needUpdate = true;
    }

    private void OnScrollChanged(Vector2 _)
    {
        _needUpdate = true;
    }

    private void UpdateCellsVisibility()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(imagesParentObject);
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

    private bool IsVisible(GalleryImage galleryImage)
    {
        galleryImage.RectTransform.GetWorldCorners(_fourCornersArray);
        var isVisibleBottomLeftCorner = RectTransformUtility.RectangleContainsScreenPoint(
            scrollRect.viewport, _fourCornersArray[0]);
        if (isVisibleBottomLeftCorner)
        {
            return true;
        }

        var isVisibleTopRightCorner = RectTransformUtility.RectangleContainsScreenPoint(
            scrollRect.viewport, _fourCornersArray[2]);
        return isVisibleTopRightCorner;
    }

    private void UpdateCellSize()
    {
        var cellSize = width
            ? Screen.width * cellSizeCft
            : Screen.height * cellSizeCft;
        grid.cellSize = Vector2.one * cellSize;
    }

    private void OnGalleryImageClick(GalleryImage clickedImage)
    {
        _gameStateMachine.EnterStateWithArgs<ViewGameState, ViewGameState.Args>(new ViewGameState.Args
        {
            GalleryImage = clickedImage,
        });
    }
}
}