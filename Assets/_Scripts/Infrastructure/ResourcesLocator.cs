using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure
{
[CreateAssetMenu(menuName = "Config/Resources Locator", fileName = "ResourcesLocator", order = 0)]
internal class ResourcesLocator : ScriptableObject
{
    [field: SerializeField]
    public AssetReference MenuSceneAsset { get; private set; }

    [field: SerializeField]
    public AssetReference GallerySceneAsset { get; private set; }

    [field: SerializeField]
    public AssetReference ViewSceneAsset { get; private set; }

    [field: SerializeField]
    public AssetReference LoadingSceneAsset { get; private set; }
}
}