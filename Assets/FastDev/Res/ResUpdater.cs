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
        public ResUpdater(string resURL)
        {
            this.resURL = resURL;
        }
        private async UniTask<bool> CheckResVersion()
        {
            string resConfigUrl = resURL + "/" + PlatformUtil.GetPlatformName() + "/ResConfig.json";
            string resConfigStr = await WebRequestManager.Instance.Get(resConfigUrl);
            var resConfig = JsonMapper.ToObject<ResConfig>(resConfigStr);
            if (resConfig == null)
            {
                Debug.LogError("Update Error! resConfig is NULL");
                onUpdateFailed?.Invoke();
                return false;
            }
            string localResConfigPath = Application.persistentDataPath + "/" + PlatformUtil.GetPlatformName() + "/ResConfig.json";
            ResConfig localResConfig = JsonMapper.ToObject<ResConfig>(FileUtil.ReadFromExternal(localResConfigPath));

            Version newVersion = Version.Parse(resConfig.resVersion);
            Version localVersion = new Version();
            if (localResConfig != null)
                localVersion = Version.Parse(localResConfig?.resVersion);

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
                    if (!await WebRequestManager.Instance.Download(fileUrl + "/" + item, savePath, (process) => onUpdateProcess?.Invoke(item, process)))
                    {
                        Debug.LogError("Update Error!");
                        onUpdateFailed?.Invoke();
                        return false;
                    }
                }
                File.WriteAllText(localResConfigPath, resConfigStr);
            }
            Debug.Log("Update Completed!");
            onUpdateSuccessed?.Invoke();
            return true;
        }

    }
}
