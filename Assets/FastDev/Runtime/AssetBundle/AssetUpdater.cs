using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FastDev
{
    public class AssetUpdater : MonoBehaviour
    {
        public string RemoteAssetUrl;

        private string RemoteAssetConfigUrl;

        private string LocalAssetPath;
        private string LocalAssetConfigPath;


        private string remoteConfig;
        private List<string> updateFiles = new List<string>();

        // Start is called before the first frame update
        void Awake()
        {
            RemoteAssetUrl = RemoteAssetUrl + "/" + Utility.Platform.GetPlatformName();

            RemoteAssetConfigUrl = RemoteAssetUrl + "/AssetConfig.json";

            LocalAssetPath = Application.persistentDataPath + "/" + Utility.Platform.GetPlatformName();

            LocalAssetConfigPath = LocalAssetPath + "/AssetConfig.json";
        }

        private async void Start()
        {
            bool result = await CheckConfig();

            if (result == true)
                result = await UpdateAsset();

            if(result == true)
                result = await LoadAssetBundle();
        }

        private async UniTask<bool> CheckConfig()
        {
            try
            {
                updateFiles.Clear();

                remoteConfig = await HttpManager.Instance.GetTxt(RemoteAssetConfigUrl).Timeout(TimeSpan.FromSeconds(5));

                if (string.IsNullOrEmpty(remoteConfig))
                {
                    throw new Exception("获取配置文件失败!");
                }

                AssetConfig remoteAssetConfig = JsonConvert.DeserializeObject<AssetConfig>(remoteConfig);

                AssetConfig localAssetConfig = new AssetConfig();

                if (File.Exists(LocalAssetConfigPath))
                {
                    localAssetConfig = JsonConvert.DeserializeObject<AssetConfig>(File.ReadAllText(LocalAssetConfigPath));
                }

                if (!string.IsNullOrEmpty(localAssetConfig.AppVersion) && localAssetConfig.AppVersion != remoteAssetConfig.AppVersion)
                {
                    Debug.LogError("需要更新APP");

                    return false;
                }

                foreach (var bundle in remoteAssetConfig.Bundles)
                {
                    if (!localAssetConfig.Bundles.ContainsKey(bundle.Key) || localAssetConfig.Bundles[bundle.Key] != bundle.Value)
                    {

                        updateFiles.Add(bundle.Key);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                Debug.LogError("配置文件读取失败！");

                return false;
            }

            return true;
        }

        private async UniTask<bool> UpdateAsset()
        {

            if (updateFiles == null || updateFiles.Count == 0)
            {
                Debug.Log("没有需要更新的资源");
                return true;
            }

            Debug.Log("开始更新资源");

            for (int i = 0; i < updateFiles.Count; i++)
            {
                bool result = await HttpManager.Instance.Download(RemoteAssetUrl + "/" + updateFiles[i], LocalAssetPath, DownloadCallback);

                if (result == false)
                {
                    Debug.LogError("下载失败，请检查网络");

                    return false;
                }
            }

            File.WriteAllText(LocalAssetConfigPath, remoteConfig);

            Debug.Log("更新完成");

            return true;
        }


        private void DownloadCallback(string fileName, float progress)
        {
            Debug.Log($"{fileName} 更新进度 {progress}");
        }


        private async UniTask<bool> LoadAssetBundle()
        {
            try
            {
                AssetConfig remoteAssetConfig = JsonConvert.DeserializeObject<AssetConfig>(remoteConfig);

                foreach (var bundle in remoteAssetConfig.Bundles)
                {
                    await AssetManager.Instance.LoadAssetBundleAsync(LocalAssetPath + "/" + bundle.Key, (progress) => LoadAssetBundleCallback(bundle.Key, progress));
                }
            }
            catch(Exception e)
            {
                Debug.LogError(e.ToString());

                return false;
            }
            Debug.Log("加载完成");
            return true;
        }

        private void LoadAssetBundleCallback(string fileName, float progress)
        {
            Debug.Log($"{fileName} 加载进度 {progress}");
        }
    }
}
