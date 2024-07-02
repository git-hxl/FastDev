
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

namespace FastDev
{
    public sealed partial class ResourceManager
    {
        private partial class ResourceUpdater
        {
            private string remoteAssetUrl;
            private string remoteAssetConfigUrl;

            private string localAssetPath;
            private string localAssetConfigPath;


            private string remoteConfig;
            private List<string> updateFiles = new List<string>();


            public ResourceUpdater()
            {
                remoteAssetUrl = remoteAssetUrl + "/" + Utility.Platform.GetPlatformName();

                remoteAssetConfigUrl = remoteAssetUrl + "/AssetConfig.json";

                localAssetPath = Application.persistentDataPath + "/" + Utility.Platform.GetPlatformName();

                localAssetConfigPath = localAssetPath + "/AssetConfig.json";
            }

            public async UniTask<bool> StartUpdate()
            {
                bool result = await CheckConfig();

                if (result)
                    result = await UpdateAsset();

                //if (result)
                //    result = await LoadAssetBundle();

                return result;
            }

            /// <summary>
            /// 检查配置文件
            /// </summary>
            /// <returns></returns>
            private async UniTask<bool> CheckConfig()
            {
                try
                {
                    updateFiles.Clear();

                    remoteConfig = await WebRequestManager.Instance.GetTxt(remoteAssetConfigUrl).Timeout(TimeSpan.FromSeconds(5));

                    if (string.IsNullOrEmpty(remoteConfig))
                    {
                        throw new Exception("获取配置文件失败!");
                    }

                    ResourceConfig remoteAssetConfig = JsonConvert.DeserializeObject<ResourceConfig>(remoteConfig);

                    ResourceConfig localAssetConfig = new ResourceConfig();

                    if (File.Exists(localAssetConfigPath))
                    {
                        localAssetConfig = JsonConvert.DeserializeObject<ResourceConfig>(File.ReadAllText(localAssetConfigPath));
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

            /// <summary>
            /// 更新资源
            /// </summary>
            /// <returns></returns>
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
                    bool result = await WebRequestManager.Instance.Download(remoteAssetUrl + "/" + updateFiles[i], localAssetPath, DownloadCallback);

                    if (result == false)
                    {
                        Debug.LogError("下载失败，请检查网络");

                        return false;
                    }
                }

                File.WriteAllText(localAssetConfigPath, remoteConfig);

                Debug.Log("更新完成");

                return true;
            }


            private void DownloadCallback(string fileName, float progress)
            {
                Debug.Log($"{fileName} 更新进度 {progress}");
            }
        }

    }
}
