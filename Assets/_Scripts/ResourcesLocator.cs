using UnityEngine;

[CreateAssetMenu(menuName = "Config/Resources Locator", fileName = "ResourcesLocator", order = 0)]
internal class ResourcesLocator : ScriptableObject
{
    [field: SerializeField]
    public SceneAssetReference MenuSceneAsset { get; private set; }

    [field: SerializeField]
    public SceneAssetReference GallerySceneAsset { get; private set; }
}