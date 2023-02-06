using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace FastDev
{
    public interface IAssetManager
    {
        AssetBundle LoadAssetBundle(string path);
        UniTask<AssetBundle> LoadAssetBundleAsync(string path,Action<float> onLoading);
        T LoadAsset<T>(string bundleName, string path) where T : UnityEngine.Object;
        UniTask<T> LoadAssetAsync<T>(string bundleName, string path) where T : UnityEngine.Object;
    }
}