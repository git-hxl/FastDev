using Cysharp.Threading.Tasks;
using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FastDev
{
    public class ResUpdater
    {
        private string remoteResUrl;

        private string remoteResConfigPath;
        private ResLoaderConfig remoteResLoaderConfig;

        private string localResConfigPath;
        private ResLoaderConfig localResLoaderConfig;

        public event Action onUpdateFailed;
        public event Action onUpdateSuccessed;
        public event Action<string, float> onUpdateProcess;

        public ResUpdater(string url)
        {
            this.remoteResUrl = url;
            localResConfigPath = Application.persistentDataPath + "/" + PlatformUtil.GetPlatformName() + "/"+ ResLoaderConfig.fileName;
            remoteResConfigPath = remoteResUrl + "/" + PlatformUtil.GetPlatformName() + "/"+ ResLoaderConfig.fileName;
        }

        private async UniTask HttpGetConfig()
        {
            localResLoaderConfig = JsonMapper.ToObject<ResLoaderConfig>(File.ReadAllText(localResConfigPath)); ;
            string txt = await HttpManager.Instance.GetTxt(remoteResConfigPath);
            if (!string.IsNullOrEmpty(txt))
                remoteResLoaderConfig = JsonMapper.ToObject<ResLoaderConfig>(txt);
        }

        public async void StartUpdateRes()
        {
            await HttpGetConfig();
            if (remoteResLoaderConfig == null)
            {
                Debug.LogError("Update Error! resLoaderConfig is NULL");
                onUpdateFailed.Invoke();
                return;
            }

            Version newVersion = Version.Parse(remoteResLoaderConfig.resVersion);
            Version localVersion = new Version();
            if (localResLoaderConfig != null)
                localVersion = Version.Parse(localResLoaderConfig.resVersion);

            Update(newVersion, localVersion).Forget();
        }

        private async UniTask Update(Version newVersion, Version localVersion)
        {
            if (newVersion > localVersion)
            {
                List<string> needUpdateFileNames = new List<string>();
                foreach (var item in remoteResLoaderConfig.resDict)
                {
                    if (localResLoaderConfig == null || !localResLoaderConfig.resDict.ContainsKey(item.Key) || localResLoaderConfig.resDict[item.Key] != item.Value)
                    {
                        needUpdateFileNames.Add(item.Key);
                    }
                }
                //开始更新
                string fileUrl = remoteResUrl + "/" + PlatformUtil.GetPlatformName();
                string localDir = Application.persistentDataPath + "/" + PlatformUtil.GetPlatformName();
                foreach (var item in needUpdateFileNames)
                {
                    Debug.Log("start to download:" + item);
                    if (!await HttpManager.Instance.Download(fileUrl + "/" + item, localDir, onUpdateProcess))
                    {
                        Debug.LogError("Update Error!");
                        onUpdateFailed?.Invoke();
                        return;
                    }
                }
                File.WriteAllText(localResConfigPath, JsonMapper.ToJson(remoteResLoaderConfig));
            }
            Debug.Log("Update Completed!");
            onUpdateSuccessed?.Invoke();
        }
    }
}
