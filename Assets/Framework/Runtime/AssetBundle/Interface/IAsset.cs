using Cysharp.Threading.Tasks;

namespace Framework
{
    internal interface IAsset
    {
        T LoadAsset<T>(string bundleName, string path) where T : UnityEngine.Object;

        UniTask<T> LoadAssetAsync<T>(string bundleName, string path) where T : UnityEngine.Object;
    }
}
