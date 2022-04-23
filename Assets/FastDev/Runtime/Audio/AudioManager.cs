using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace FastDev.Audio
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        private string settingPath;
        private VolumeSetting _volumeSetting;
        public VolumeSetting volumeSetting
        {
            get { return _volumeSetting; }
        }

        private Dictionary<string, IAudioPlayer> audioPlayers = new Dictionary<string, IAudioPlayer>();
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

        public IAudioPlayer GetAudioPlayer(string assetPath, AudioType audioType)
        {
            IAudioPlayer audioPlayer;
            if (audioPlayers.TryGetValue(assetPath, out audioPlayer))
            {
                if (audioPlayer == null || audioPlayer.audioSource == null)
                    audioPlayers.Remove(assetPath);
            }
            if (audioPlayer == null || audioPlayer.audioSource == null)
            {
                audioPlayer = new AudioPlayer(audioType, assetPath);
                audioPlayers.Add(assetPath, audioPlayer);
            }
            return audioPlayer;
        }

        public void SaveSetting()
        {
            File.WriteAllText(settingPath, volumeSetting.ToJson());
        }

        public override void Dispose()
        {
            base.Dispose();
            audioPlayers.Clear();
            Resources.UnloadUnusedAssets();
        }

    }
}