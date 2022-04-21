using System.IO;
using UnityEngine;
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
                _volumeSetting = txt.ToObjectByJson<VolumeSetting>();
            }
        }

        public void SaveSetting()
        {
            File.WriteAllText(settingPath, volumeSetting.ToJson());
        }

        public override void Dispose()
        {
            base.Dispose();
            Resources.UnloadUnusedAssets();
        }

    }
}