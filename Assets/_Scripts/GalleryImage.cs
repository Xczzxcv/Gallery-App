using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal class GalleryImage : UIBehaviour
{
    [SerializeField]
    private Image target;

    [field:SerializeField]
    public RectTransform RectTransform { get; private set; }

    public bool Seen { get; private set; }
    public bool HaveImg { get; private set; }
    public string ImageUrl { get; private set; }

    public void Setup(string imageUrl)
    {
        ImageUrl = imageUrl;
    }

    public void SetImg(Sprite sprite)
    {
        target.sprite = sprite;
        HaveImg = true;
    }

    public void MarkSeen()
    {
        Seen = true;
    }
}