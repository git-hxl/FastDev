using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Cysharp.Threading.Tasks;
namespace Bigger
{
    public class ResManager : MonoSingleton<ResManager>
    {
        public ResLoadType resLoadType;
        private string resDir;
        private string resConfigPath;
        private ResConfig ResConfig;
        private Dictionary<string, AssetBundle> bundles = new Dictionary<string, AssetBundle>();

        protected override void Awake()
        {
            base.Awake();
            Init().Forget();
        }
        /// <summary>
        /// 读取配置文件并开始加载对应AB包
        /// </summary>
        /// <returns></returns>
        [ContextMenu("Reload")]
        private async UniTaskVoid Init()
        {
            if (!PlatformUtil.IsEditor && resLoadType == ResLoadType.FromEditor)
                resLoadType = ResLoadType.FromStreamingAssets;
            switch (resLoadType)
            {
                case ResLoadType.FromEditor:
                    return;
                case ResLoadType.FromStreamingAssets:
                    resDir = Application.streamingAssetsPath + "/" + PlatformUtil.GetPlatformName();
                    break;
                case ResLoadType.FromPersistentPath:
                    resDir = Application.persistentDataPath + "/" + PlatformUtil.GetPlatformName();
                    break;
            }
            resConfigPath = resDir + "/resConfig.txt";
            string resConfigText = await FileUtil.ReadFile(resConfigPath);
            if(string.IsNullOrEmpty(resConfigText))
            {
                return;
            }
            ResConfig = JsonMapper.ToObject<ResConfig>(resConfigText);
            await LoadAllAssetBundle();
        }

        private async UniTask LoadAllAssetBundle()
        {
            foreach (var item in ResConfig.resDict)
            {
                var assetBundle = await LoadAssetBundle(item.Key);
                bundles.Add(item.Key, assetBundle);
            }
        }
        /// <summary>
        /// 加载AssetBundle
        /// </summary>
        /// <param name="bundleName"></param>
        /// <returns></returns>
        public async UniTask<AssetBundle> LoadAssetBundle(string bundleName)
        {
            Debug.Log("加载AssetBundle:" + bundleName);
            string path = resDir + "/" + bundleName;
            AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(path);
            await request;
            return request.assetBundle;
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