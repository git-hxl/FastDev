using System.IO;
using UnityEngine;
using LitJson;
namespace FastDev.Audio
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        private VolumeSetting _volumeSetting;
        public VolumeSetting volumeSetting
        {
            get { return _volumeSetting; }
        }
        private string settingPath;

        protected override void OnInit()
        {
            settingPath = Application.persistentDataPath + "/audioSetting.txt";
            _volumeSetting = new VolumeSetting();
            if (File.Exists(settingPath))
            {
                string txt = File.ReadAllText(settingPath);
                _volumeSetting = JsonMapper.ToObject<VolumeSetting>(txt);
            }
        }

        public void SaveSetting()
        {
            File.WriteAllText(settingPath, JsonMapper.ToJson(volumeSetting));
        }

        public override void Dispose()
        {
            base.Dispose();
            Resources.UnloadUnusedAssets();
        }

    }
}