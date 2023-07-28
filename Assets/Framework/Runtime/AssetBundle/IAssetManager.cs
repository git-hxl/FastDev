
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace GameFramework
{
    internal interface IAssetManager
    {
        AssetBundle LoadAssetBundle(string path);

        UniTask<AssetBundle> LoadAssetBundleAsync(string path, Action<float> onLoading);

        void UnloadAssetBundle(string bundleName, bool unloadAll);

        T LoadAsset<T>(string bundleName, string path) where T : UnityEngine.Object;

        UniTask<T> LoadAssetAsync<T>(string bundleName, string path) where T : UnityEngine.Object;
    }
}
