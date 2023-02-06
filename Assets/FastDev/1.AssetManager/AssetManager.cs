using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FastDev
{
    public class AssetManager : MonoSingleton<AssetManager>, IAssetManager
    {
        private Dictionary<string, AssetBundle> assetBundleDict = new Dictionary<string, AssetBundle>();

        private void AddAssetBundle(string bundleName, AssetBundle assetBundle)
        {
            assetBundleDict[bundleName] = assetBundle;
        }

        public T LoadAsset<T>(string bundleName, string path) where T : UnityEngine.Object
        {
            T asset = default(T);
#if UNITY_EDITOR
            asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
#else
            if (assetBundleDict.ContainsKey(bundleName))
            {
                var assetBundle = assetBundleDict[bundleName];
                var assetName = Path.GetFileName(path);
                asset = assetBundle.LoadAsset<T>(assetName);
            }
#endif
            if (asset == null)
                Debug.LogError("asset load failed: " + bundleName);
            return asset;
        }

        public async UniTask<T> LoadAssetAsync<T>(string bundleName, string path) where T : UnityEngine.Object
        {
            T asset = default(T);
#if UNITY_EDITOR
            asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
            await UniTask.DelayFrame(1);
#else
            if (assetBundleDict.ContainsKey(bundleName))
            {
                var assetBundle = assetBundleDict[bundleName];
                var assetName = Path.GetFileName(path);
                asset = await assetBundle.LoadAssetAsync<T>(assetName) as T;
            }
#endif
            if (asset == null)
                Debug.LogError("asset load failed: " + bundleName);
            return asset;
        }

        public AssetBundle LoadAssetBundle(string path)
        {
            AssetBundle assetBundle = AssetBundle.LoadFromFile(path);
            AddAssetBundle(Path.GetFileName(path), assetBundle);
            return assetBundle;
        }

        public async UniTask<AssetBundle> LoadAssetBundleAsync(string path, Action<float> onLoading)
        {
            AssetBundle assetBundle = await AssetBundle.LoadFromFileAsync(path).ToUniTask(Progress.Create(onLoading));
            AddAssetBundle(Path.GetFileName(path), assetBundle);
            return assetBundle;
        }
    }
}