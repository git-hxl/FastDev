using System.IO;
using UnityEngine;
using LitJson;
using System.Collections.Generic;

namespace Bigger
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoSingleton<AudioManager>
    {
        private AudioSetting audioSetting;
        private string settingPath;
        private AudioSource audioSource;
        private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
        protected override void Awake()
        {
            base.Awake();
            Init();
        }
        private void Init()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            settingPath = Application.persistentDataPath + "/audioSetting.txt";
            audioSetting = new AudioSetting();
            if (File.Exists(settingPath))
            {
                string txt = File.ReadAllText(settingPath);
                audioSetting = JsonMapper.ToObject<AudioSetting>(txt);
            }
            audioSource.volume = MusicVolume;
        }
        public float TotalVolume
        {
            get { return audioSetting.totalVolume; }
            set
            {
                audioSetting.totalVolume = value;
                audioSource.volume = MusicVolume;
            }
        }
        public float SoundVolume
        {
            get { return audioSetting.totalVolume * audioSetting.soundVolume; }
            set
            {
                audioSetting.soundVolume = value;
            }
        }
        public float MusicVolume
        {
            get { return audioSetting.totalVolume * audioSetting.musicVolume; }
            set
            {
                audioSetting.musicVolume = value;
                audioSource.volume = MusicVolume;
            }
        }

        public void PlayClip(string assetPath)
        {
            AudioClip clip = GetAudioClip(assetPath);
            audioSource.PlayOneShot(clip, SoundVolume);
        }

        public void PlayClipAtPoint(string assetPath, Vector3 pos)
        {
            AudioClip clip = GetAudioClip(assetPath);
            AudioSource.PlayClipAtPoint(clip, pos, SoundVolume);
        }

        public void PlayMusic(string assetPath)
        {
            AudioClip clip = GetAudioClip(assetPath);
            audioSource.clip = clip;
            audioSource.Play();
        }

        public AudioClip GetAudioClip(string assetPath)
        {
            AudioClip audioClip = null;
            if (!audioClips.TryGetValue(assetPath, out audioClip))
            {
                audioClip = ResManager.Instance.LoadAsset<AudioClip>(BundleConstant.Audio, assetPath);
                audioClips.Add(assetPath, audioClip);
            }
            return audioClip;
        }

        public void SaveSetting()
        {
            File.WriteAllText(settingPath, JsonMapper.ToJson(audioSetting));
        }

        public override void Dispose()
        {
            audioClips.Clear();
            Resources.UnloadUnusedAssets();
        }

    }
}