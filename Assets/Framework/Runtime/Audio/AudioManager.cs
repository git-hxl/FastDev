using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Framework
{
    public class AudioManager : MonoSingleton<AudioManager>, IAudioManager
    {
        public AudioSetting Setting { get; private set; }
        public string SettingPath { get; private set; }
        public List<AudioPlayer> AudioPlayers { get; private set; }

        protected override void OnInit()
        {
            base.OnInit();
            Setting = new AudioSetting();
            SettingPath = Application.streamingAssetsPath + "/AudioSetting.json";
            AudioPlayers = new List<AudioPlayer>();
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

        public AudioPlayer CreateAudioPlayer(AudioType audioType)
        {
            AudioPlayer audioPlayer = gameObject.AddComponent<AudioPlayer>();
            audioPlayer.AudioType = audioType;
            return audioPlayer;
        }

        public AudioPlayer GetAudioPlayer(AudioType audioType)
        {
            AudioPlayer audioPlayer = this.AudioPlayers.FirstOrDefault((a) => a.AudioType == audioType);
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

        public void RegisterAudioPlayer(AudioPlayer audioPlayer)
        {
            if (!AudioPlayers.Contains(audioPlayer))
            {
                AudioPlayers.Add(audioPlayer);
            }
        }

        public void UnRegisterAudioPlayer(AudioPlayer audioSoundPlayer)
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
            File.WriteAllText(SettingPath, json);
        }

        private void OnDestroy()
        {
            SaveSetting();
        }
    }
}