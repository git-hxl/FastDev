using Cysharp.Threading.Tasks;
using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FastDev.Res
{
    public class ResUpdater
    {
        private string resURL;
        public Action onUpdateFailed;
        public Action onUpdateSuccessed;
        public Action<string, float> onUpdateProcess;

        private string localResConfigPath;
        public ResUpdater(string resURL)
        {
            this.resURL = resURL;
            localResConfigPath = Application.persistentDataPath + "/" + PlatformUtil.GetPlatformName() + "/ResConfig.json";
        }

        private void GetConfig()
        {
            string resConfigUrl = resURL + "/" + PlatformUtil.GetPlatformName() + "/ResConfig.json";
            HttpManager.Instance.Get(resConfigUrl, CheckResVersion).Forget();
        }

        private void CheckResVersion(string strConfig)
        {
            ResLoaderConfig resConfig = JsonMapper.ToObject<ResLoaderConfig>(strConfig);
            if (resConfig == null)
            {
                Debug.LogError("Update Error! resConfig is NULL");
                onUpdateFailed?.Invoke();
                return;
            }
        
            ResLoaderConfig localResConfig = JsonMapper.ToObject<ResLoaderConfig>(File.ReadAllText(localResConfigPath));

            Version newVersion = Version.Parse(resConfig.resVersion);
            Version localVersion = new Version();
            if (localResConfig != null)
                localVersion = Version.Parse(localResConfig.resVersion);

            Update(newVersion, localVersion,resConfig,localResConfig).Forget();
        }

        private async UniTask<bool> Update(Version newVersion, Version localVersion, ResLoaderConfig resConfig, ResLoaderConfig localResConfig)
        {
            if (newVersion > localVersion)
            {
                List<string> needUpdateFileNames = new List<string>();
                foreach (var item in resConfig.resDict)
                {
                    if (localResConfig == null || !localResConfig.resDict.ContainsKey(item.Key) || localResConfig.resDict[item.Key] != item.Value)
                    {
                        needUpdateFileNames.Add(item.Key);
                    }
                }
                //开始更新
                string fileUrl = resURL + "/" + PlatformUtil.GetPlatformName();
                string savePath = Application.persistentDataPath + "/" + PlatformUtil.GetPlatformName();
                foreach (var item in needUpdateFileNames)
                {
                    Debug.Log("start to download:" + item);
                    if (!await HttpManager.Instance.Download(fileUrl + "/" + item, savePath, (process) => onUpdateProcess?.Invoke(item, process)))
                    {
                        Debug.LogError("Update Error!");
                        onUpdateFailed?.Invoke();
                        return false;
                    }
                }
                File.WriteAllText(localResConfigPath, resConfig.ToJson());
            }
            Debug.Log("Update Completed!");
            onUpdateSuccessed?.Invoke();
            return true;
        }
    }
}
