using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Framework
{
    internal interface IAssetBundle
    {
        AssetBundle LoadAssetBundle(string path);

        UniTask<AssetBundle> LoadAssetBundleAsync(string path, Action<float> onLoading);

        void UnloadAssetBundle(string bundleName,bool unloadAll);
    }
}
