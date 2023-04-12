using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FastDev
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        public AudioPlayer DefaultUIPlayer { get; private set; }
        public AudioPlayer DefaultSoundPlayer { get; private set; }
        public AudioPlayer DefaultVoicePlayer { get; private set; }
        public AudioPlayer DefaultMusicPlayer { get; private set; }

        public AudioSetting Setting { get; private set; }

        public List<AudioPlayer> Players { get; private set; } = new List<AudioPlayer>();

        protected override void OnInit()
        {
            base.OnInit();
            InitSetting();
            InitDefaultAudio();
        }

        private void InitSetting()
        {
            string path = Application.streamingAssetsPath + "/AudioSetting.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Setting = JsonConvert.DeserializeObject<AudioSetting>(json);
            }
        }

        private void InitDefaultAudio()
        {
            DefaultMusicPlayer = gameObject.AddComponent<AudioPlayer>();
            DefaultMusicPlayer.AudioType = AudioType.Music;

            DefaultUIPlayer = gameObject.AddComponent<AudioPlayer>();
            DefaultUIPlayer.AudioType = AudioType.UI;

            DefaultSoundPlayer = gameObject.AddComponent<AudioPlayer>();
            DefaultSoundPlayer.AudioType = AudioType.Sound;

            DefaultVoicePlayer = gameObject.AddComponent<AudioPlayer>();
            DefaultVoicePlayer.AudioType = AudioType.Voice;
        }

        [ContextMenu("SaveSetting")]
        public void SaveSetting()
        {
            if (Setting == null)
                Setting = new AudioSetting();
            string path = Application.streamingAssetsPath + "/AudioSetting.json";
            string json = JsonConvert.SerializeObject(Setting, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public AudioClip LoadAudioClip(string path)
        {
            return AssetManager.Instance.LoadAsset<AudioClip>("audio", path);
        }

        public void PlaySoundAtPosition(Vector3 pos, string path)
        {
            AudioClip clip = LoadAudioClip(path);
            AudioSource.PlayClipAtPoint(clip, pos, GetVolume(AudioType.Sound));
        }

        public float GetVolume(AudioType audioSoundType)
        {
            switch (audioSoundType)
            {
                case AudioType.Sound:
                    return Setting.TotalVolume * Setting.SoundVolume;
                case AudioType.UI:
                    return Setting.TotalVolume * Setting.UIVolume;
                case AudioType.Music:
                    return Setting.TotalVolume * Setting.MusicVolume;
                case AudioType.Voice:
                    return Setting.TotalVolume * Setting.VoiceVolume;
            }
            return Setting.TotalVolume;
        }

        public void UpdateVolume()
        {
            foreach (var player in Players)
            {
                if (player != null && player.AudioSource != null)
                {
                    player.AudioSource.volume = GetVolume(player.AudioType);
                }
            }
        }

        public void RegisterAudioPlayer(AudioPlayer audioPlayer)
        {
            if (!Players.Contains(audioPlayer))
            {
                Players.Add(audioPlayer);
            }
        }

        public void UnRegisterAudioPlayer(AudioPlayer audioSoundPlayer)
        {
            if (Players.Contains(audioSoundPlayer))
            {
                Players.Remove(audioSoundPlayer);
            }
        }
    }
}