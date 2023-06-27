using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal class GalleryImage : UIBehaviour
{
    private enum State
    {
        Blank = 0,
        Seen = 1,
        Loaded = 2
    }

    [SerializeField]
    private Image target;
    [SerializeField]
    private Button btn;

    [field:SerializeField]
    public RectTransform RectTransform { get; private set; }

    private State _state;
    public string ImageUrl { get; private set; }
    public Sprite Sprite => target.sprite;
    public bool Seen => _state >= State.Seen;

    public event Action<GalleryImage> ImgClick;

    protected override void Awake()
    {
        btn.onClick.AddListener(OnImgClick);
    }

    private void OnImgClick()
    {
        if (_state == State.Loaded)
        {
            ImgClick?.Invoke(this);
        }
    }

    public void Setup(string imageUrl)
    {
        ImageUrl = imageUrl;
    }

    public void SetImg(Sprite sprite)
    {
        target.sprite = sprite;
        target.color = Color.white;
        btn.interactable = true;
        _state = State.Loaded;
    }

    public void MarkSeen()
    {
        if (_state < State.Seen)
        {
            _state = State.Seen;
        }
    }
}