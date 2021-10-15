using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Cysharp.Threading.Tasks;
using System;
using System.IO;

namespace Bigger
{
    public class ResManager : MonoSingleton<ResManager>
    {
        public ResLoadType resLoadType;
        /// <summary>
        /// 获取最新版本的地址
        /// </summary>
        public string versionUrl;
        public bool checkUpdate;
        public Action<string, float> downloadCallback;
        public Action updateErrorCallback;
        public Action initSuccessCallback;
        private string resDir;
        private string resConfigPath;
        private ResConfig resConfig;
        private Dictionary<string, AssetBundle> bundles = new Dictionary<string, AssetBundle>();
        private async UniTaskVoid Start()
        {
            if (!PlatformUtil.IsEditor || checkUpdate && !string.IsNullOrEmpty(versionUrl))
            {
                if (!await CheckResVersion())
                {
                    updateErrorCallback?.Invoke();
                    return;
                }
            }
            await Init();
            initSuccessCallback?.Invoke();
        }

        private async UniTask<bool> CheckResVersion()
        {
            string resUrl = await WebRequestManager.Instance.Get(versionUrl);
            if (string.IsNullOrEmpty(resUrl))
            {
                Debug.LogError("Update Error!");
                return false;
            }
            string resConfigUrl = resUrl + "/" + PlatformUtil.GetPlatformName() + "/resConfig.txt";
            string resConfigStr = await WebRequestManager.Instance.Get(resConfigUrl);
            resConfig = JsonMapper.ToObject<ResConfig>(resConfigStr);
            if (resConfig == null)
            {
                Debug.LogError("Update Error!");
                return false;
            }
            string localResConfigPath = Application.persistentDataPath + "/" + PlatformUtil.GetPlatformName() + "/resConfig.txt";
            ResConfig localResConfig = JsonMapper.ToObject<ResConfig>(FileUtil.ReadFromExternal(localResConfigPath));
            List<string> needUpdateFileNames = new List<string>();
            foreach (var item in resConfig.resDict)
            {
                if (localResConfig == null || !localResConfig.resDict.ContainsKey(item.Key) || localResConfig.resDict[item.Key] != item.Value)
                {
                    needUpdateFileNames.Add(item.Key);
                }
            }
            //开始更新
            string fileUrl = resUrl + "/" + PlatformUtil.GetPlatformName();
            string savePath = Application.persistentDataPath + "/" + PlatformUtil.GetPlatformName();
            foreach (var item in needUpdateFileNames)
            {
                Debug.Log("start to download:" + item);
                if (!await WebRequestManager.Instance.Download(fileUrl + "/" + item, savePath, (process) => downloadCallback?.Invoke(item, process)))
                {
                    Debug.LogError("Update Error!");
                    return false;
                }
            }
            File.WriteAllText(localResConfigPath, resConfigStr);
            Debug.Log("Update Completed!");
            return true;
        }
        /// <summary>
        /// 读取配置文件并开始加载对应AB包
        /// </summary>
        /// <returns></returns>
        [ContextMenu("Reload")]
        private async UniTask Init()
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
            if (!string.IsNullOrEmpty(resConfigText))
            {
                resConfig = JsonMapper.ToObject<ResConfig>(resConfigText);
                await LoadAllAssetBundle();
            }
        }

        private async UniTask LoadAllAssetBundle()
        {
            foreach (var item in resConfig.resDict)
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
            Debug.Log("Load AssetBundle:" + bundleName);
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