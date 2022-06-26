using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FastDev
{
    public class ResLoader : Singleton<ResLoader>
    {
        public ResLoaderType ResLoaderType;

        private string assetPath;
        private ResLoaderConfig resLoaderConfig;
        private AssetBundleManifest assetBundleManifest;
        private Dictionary<string, AssetBundle> bundles = new Dictionary<string, AssetBundle>();

        public ResLoader()
        {
            switch (ResLoaderType)
            {
                case ResLoaderType.FromPersistentPath:
                    assetPath = Application.persistentDataPath + "/" + PlatformUtil.GetPlatformName();
                    break;
                case ResLoaderType.FromStreamingAssets:
                    assetPath = Application.streamingAssetsPath + "/" + PlatformUtil.GetPlatformName();
                    break;
            }

            if (!string.IsNullOrEmpty(assetPath))
            {
                string config = File.ReadAllText(assetPath + "/" + ResLoaderConfig.fileName);
                resLoaderConfig = LitJson.JsonMapper.ToObject<ResLoaderConfig>(config);
            }
        }

        [ContextMenu("Load All Bundles")]
        public async UniTask LoadAllAssetBundle()
        {
            if (resLoaderConfig == null)
                return;
            foreach (var item in assetBundleManifest.GetAllAssetBundles())
            {
                var assetBundle = await LoadAssetBundle(item);
                bundles.Add(item, assetBundle);
            }
        }

        /// <summary>
        /// 加载AssetBundle
        /// </summary>
        /// <param name="assetBundle"></param>
        /// <returns></returns>
        public async UniTask<AssetBundle> LoadAssetBundle(string assetBundle)
        {
            Debug.Log("Load AssetBundle:" + assetBundle);
            AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(assetPath + "/" + assetBundle);
            await request;
            return request.assetBundle;
        }

        public string[] GetAllDependencies(string bundleName)
        {
            return assetBundleManifest.GetAllDependencies(bundleName);
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bundleName"></param>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        public T LoadAsset<T>(string bundleName, string assetPath) where T : UnityEngine.Object
        {
            if (ResLoaderType == ResLoaderType.FromEditor)
            {
                return LoadAssetFromEditor<T>(assetPath);
            }
            return LoadAssetFromAB<T>(bundleName, assetPath);
        }

        /// <summary>
        /// 卸载AssetBundle
        /// </summary>
        /// <param name="bundleName"></param>
        /// <param name="unloadLoadedObjects"></param>
        public void UnloadAssetBundle(string bundleName, bool unloadLoadedObjects = true)
        {
            if (bundles.ContainsKey(bundleName))
            {
                bundles[bundleName].Unload(unloadLoadedObjects);
                bundles.Remove(bundleName);
            }
        }

        private T LoadAssetFromEditor<T>(string assetPath) where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            T asset = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (asset != null)
            {
                return asset;
            }
#endif
            Debug.LogError("Res Load Failed! " + assetPath);
            return null;
        }

        private T LoadAssetFromAB<T>(string bundleName, string assetPath) where T : UnityEngine.Object
        {
            if (bundles.ContainsKey(bundleName))
            {
                var assetBundle = bundles[bundleName];
                var assetName = assetPath.GetFileName();
                if (assetBundle.Contains(assetName))
                {
                    return assetBundle.LoadAsset<T>(assetName);
                }
            }
            Debug.LogError("Res Load Failed! " + assetPath);
            return null;
        }

        [ContextMenu("Unload All")]
        public override void Dispose()
        {
            base.Dispose();
            foreach (var item in bundles)
            {
                item.Value.Unload(true);
            }
            bundles.Clear();
        }

        [ContextMenu("Unload UnUsed")]
        public void UnloadUnUsedAssets()
        {
            Resources.UnloadUnusedAssets();
        }

    }
}