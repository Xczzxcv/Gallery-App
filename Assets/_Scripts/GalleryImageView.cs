using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal class GalleryImageView : UIBehaviour
{
    [SerializeField]
    private Image img;
    [SerializeField]
    private Button exitBtn;
    [SerializeField]
    private GameObject rootObject;

    public event Action ExitBtnClick;

    protected override void Awake()
    {
        exitBtn.onClick.AddListener(OnExitBtnClick);
    }

    public void Show(GalleryImage galleryImage)
    {
        img.sprite = galleryImage.Sprite;
        rootObject.SetActive(true);
    }

    public void Hide()
    {
        rootObject.SetActive(false);
    }

    private void OnExitBtnClick()
    {
        ExitBtnClick?.Invoke();
    }
}