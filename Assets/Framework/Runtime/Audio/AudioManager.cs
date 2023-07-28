using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace GameFramework
{
    public class AudioManager : MonoSingleton<AudioManager>, IAudioManager
    {
        public AudioSetting Setting { get; private set; }
        public string SettingPath { get; private set; }
        public List<IAudioPlayer> AudioPlayers { get; private set; } = new List<IAudioPlayer>();

        protected override void OnInit()
        {
            base.OnInit();
            Setting = new AudioSetting();
            SettingPath = Application.streamingAssetsPath + "/AudioSetting.json";
            if (File.Exists(SettingPath))
            {
                string json = File.ReadAllText(SettingPath);
                Setting = JsonConvert.DeserializeObject<AudioSetting>(json);
            }
        }

        public AudioClip LoadAudioClip(string path)
        {
            return AssetManager.Instance.LoadAsset<AudioClip>("audio", path);
        }

        public IAudioPlayer CreateAudioPlayer(AudioType audioType)
        {
            IAudioPlayer audioPlayer = gameObject.AddComponent<AudioPlayer>();
            audioPlayer.AudioType = audioType;

            RegisterAudioPlayer(audioPlayer);

            return audioPlayer;
        }

        public IAudioPlayer GetAudioPlayer(AudioType audioType)
        {
            IAudioPlayer audioPlayer = this.AudioPlayers.FirstOrDefault((a) => a.AudioType == audioType);
            if (audioPlayer == null)
            {
                audioPlayer = CreateAudioPlayer(audioType);
            }
            return audioPlayer;
        }

        public float GetVolume(AudioType audioType)
        {
            return Setting.AudioTypeVolume[audioType] * Setting.TotalVolume;
        }

        public void UpdateVolume()
        {
            foreach (var player in AudioPlayers)
            {
                if (player != null && player.AudioSource != null)
                {
                    player.AudioSource.volume = GetVolume(player.AudioType);
                }
            }
        }

        public void RegisterAudioPlayer(IAudioPlayer audioPlayer)
        {
            if (!AudioPlayers.Contains(audioPlayer))
            {
                AudioPlayers.Add(audioPlayer);
            }
        }

        public void UnRegisterAudioPlayer(IAudioPlayer audioSoundPlayer)
        {
            if (AudioPlayers.Contains(audioSoundPlayer))
            {
                AudioPlayers.Remove(audioSoundPlayer);
            }
        }

        [ContextMenu("SaveSetting")]
        public void SaveSetting()
        {
            if (Setting == null)
                Setting = new AudioSetting();
            string json = JsonConvert.SerializeObject(Setting, Formatting.Indented);
            if (!string.IsNullOrEmpty(SettingPath))
                File.WriteAllText(SettingPath, json);
        }

        private void OnDestroy()
        {
            SaveSetting();
        }
    }
}