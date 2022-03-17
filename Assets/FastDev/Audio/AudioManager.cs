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
        protected override void Init()
        {
            base.Init();
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

        public IAudioPlayer GetDefaultAudioPlayer()
        {
            return new AudioPlayer(AudioSetting.Default);
        }

        public IAudioPlayer GetMusicAudioPlayer()
        {
            return new AudioPlayer(AudioSetting.Music);
        }

        public override void Dispose()
        {
            base.Dispose();
            Resources.UnloadUnusedAssets();
        }

    }
}