using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        public AudioManagerSetting Setting { get; private set; }
        public AudioPlayer DefaultMusicPlayer { get; private set; }
        public AudioPlayer DefaultUIPlayer { get; private set; }
        public List<AudioPlayer> Players { get; private set; } = new List<AudioPlayer>();

        protected override void Awake()
        {
            base.Awake();
            Setting = AudioManagerSetting.Init();

            DefaultMusicPlayer = new AudioPlayer(AudioType.Music, gameObject);
            DefaultUIPlayer = new AudioPlayer(AudioType.UI, gameObject);
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