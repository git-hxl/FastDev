
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using Newtonsoft.Json;

namespace FastDev
{
    public sealed partial class ResourceManager : GameModule
    {
        private Dictionary<string, AssetBundle> bundles;

        public ResourceManager()
        {
            bundles = new Dictionary<string, AssetBundle>();
        }

        /// <summary>
        /// 添加AssetBundle
        /// </summary>
        /// <param name="bundleName"></param>
        /// <param name="assetBundle"></param>
        private void AddAssetBundle(string bundleName, AssetBundle assetBundle)
        {
            if (bundles.ContainsKey(bundleName))
            {
                Debug.Log("bundle is existed! check it!");
            }
            bundles[bundleName] = assetBundle;
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bundleName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bundleName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
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

        public async UniTask<bool> LoadAllAssetBundle(Action<string, float> onProgress)
        {
            try
            {
                string localAssetPath = Application.persistentDataPath + "/" + Utility.Platform.GetPlatformName();

                string localAssetConfigPath = localAssetPath + "/ResourceConfig.json";

                ResourceConfig resourceConfig = JsonConvert.DeserializeObject<ResourceConfig>(localAssetConfigPath);

                foreach (var bundle in resourceConfig.Bundles)
                {
                    AssetBundle assetBundle = await LoadAssetBundleAsync(localAssetPath + "/" + bundle.Key, (progress) => onProgress(bundle.Key, progress));

                    AddAssetBundle(bundle.Key, assetBundle);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());

                return false;
            }
            Debug.Log("加载完成");
            return true;
        }

        /// <summary>
        /// 加载AssetBundle
        /// </summary>
        /// <param name="path"></param>
        /// <param name="onLoading"></param>
        /// <returns></returns>
        public async UniTask<AssetBundle> LoadAssetBundleAsync(string path, Action<float> onLoading)
        {
            AssetBundle assetBundle = await AssetBundle.LoadFromFileAsync(path).ToUniTask(Progress.Create(onLoading));
            AddAssetBundle(Path.GetFileName(path), assetBundle);
            return assetBundle;
        }

        /// <summary>
        /// 卸载AssetBundle
        /// </summary>
        /// <param name="bundleName"></param>
        /// <param name="unloadAll"></param>
        public void UnloadAssetBundle(string bundleName, bool unloadAll)
        {
            if (bundles.ContainsKey(bundleName))
            {
                AssetBundle bundle = bundles[bundleName];
                if (bundle != null)
                {
                    bundle.Unload(unloadAll);
                }
                bundles.Remove(bundleName);
            }
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <returns></returns>
        public ResourceConfig LoadConfig()
        {
            string path = Application.persistentDataPath + "/" + Utility.Platform.GetPlatformName() + "/AssetConfig.json";
            if (File.Exists(path))
            {
                ResourceConfig config = JsonConvert.DeserializeObject<ResourceConfig>(File.ReadAllText(path));
                return config;
            }
            return null;
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            //throw new NotImplementedException();
        }

        internal override void Shutdown()
        {
            //throw new NotImplementedException();

            foreach (var item in bundles)
            {
                UnloadAssetBundle(item.Key, true);
            }

            bundles.Clear();
        }
    }
}
