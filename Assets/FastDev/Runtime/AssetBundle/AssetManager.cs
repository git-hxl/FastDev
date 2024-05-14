
//#define ForceLoadFromAssetBundle //强制从AB包中加载资源

using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;


namespace FastDev
{
    public class AssetManager : Singleton<AssetManager>, IAssetManager
    {
        public Dictionary<string, AssetBundle> AssetBundles { get; private set; }

        protected override void OnInit()
        {
            base.OnInit();
            AssetBundles = new Dictionary<string, AssetBundle>();
        }

        private void AddAssetBundle(string bundleName, AssetBundle assetBundle)
        {
            if (AssetBundles.ContainsKey(bundleName))
            {
                Debug.LogError("bundle is existed! check it!");
            }
            AssetBundles[bundleName] = assetBundle;
        }

        public T LoadAsset<T>(string bundleName, string path) where T : UnityEngine.Object
        {
            T asset = default(T);

#if UNITY_EDITOR
            asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
#else
            if (AssetBundles.ContainsKey(bundleName))
            {
                var assetBundle = AssetBundles[bundleName];
                var assetName = Path.GetFileName(path);
                asset = assetBundle.LoadAsset<T>(assetName);
            }
#endif
            if (asset == null)
                Debug.LogError("asset load failed: " + path);
            return asset;
        }

        public async UniTask<T> LoadAssetAsync<T>(string bundleName, string path) where T : UnityEngine.Object
        {
            T asset = default(T);
#if UNITY_EDITOR
            asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
            await UniTask.DelayFrame(1);
#else
            if (AssetBundles.ContainsKey(bundleName))
            {
                var assetBundle = AssetBundles[bundleName];
                var assetName = Path.GetFileName(path);
                asset = await assetBundle.LoadAssetAsync<T>(assetName) as T;
            }
#endif
            if (asset == null)
                Debug.LogError("asset load failed: " + path);
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

        public void UnloadAssetBundle(string bundleName, bool unloadAll)
        {
            if (AssetBundles.ContainsKey(bundleName))
            {
                AssetBundle bundle = AssetBundles[bundleName];
                if (bundle != null)
                {
                    bundle.Unload(unloadAll);
                }
                AssetBundles.Remove(bundleName);
            }
        }

        public ABConfig LoadConfig()
        {
            string path = Application.persistentDataPath + "/" + Utility.Platform.GetPlatformName() + "/AssetConfig.json";
            if (File.Exists(path))
            {
                ABConfig config = JsonConvert.DeserializeObject<ABConfig>(File.ReadAllText(path));
                return config;
            }
            return null;
        }

    }
}