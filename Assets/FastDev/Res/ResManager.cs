using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
namespace FastDev.Res
{
    public class ResManager : MonoSingleton<ResManager>
    {
        public ResLoadType resLoadType;
        private string assetPath;
        private AssetBundleManifest assetBundleManifest;
        private Dictionary<string, AssetBundle> bundles = new Dictionary<string, AssetBundle>();
        protected override void Init()
        {
            base.Init();
            switch (resLoadType)
            {
                case ResLoadType.FromPersistentPath:
                    assetPath = Application.persistentDataPath + "/" + PlatformUtil.GetPlatformName();
                    break;
                case ResLoadType.FromStreamingAssets:
                    assetPath = Application.streamingAssetsPath + "/" + PlatformUtil.GetPlatformName();
                    break;
            }
            if (!string.IsNullOrEmpty(assetPath))
            {
                AssetBundle ab = AssetBundle.LoadFromFile(assetPath + "/" + PlatformUtil.GetPlatformName());
                assetBundleManifest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            }
        }

        public async UniTask LoadAllAssetBundle()
        {
            if (assetBundleManifest == null)
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
            if (resLoadType == ResLoadType.FromEditor)
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