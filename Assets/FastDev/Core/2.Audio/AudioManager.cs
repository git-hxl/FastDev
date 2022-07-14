using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace FastDev
{
    public class AudioManager : MonoSingleton<AudioManager>, IAudioManager
    {
        public AudioSetting AudioSetting;
        private string bundleName = "audio";
        private string settingPath;
        private AudioSource audioSource;
        public AudioSource AudioSource
        {
            get
            {
                if (audioSource == null)
                {
                    audioSource = gameObject.AddComponent<AudioSource>();
                }
                return audioSource;
            }
        }
        private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

        protected override void Awake()
        {
            base.Awake();
            settingPath = Application.persistentDataPath + "/audioSetting.txt";
            try
            {
                AudioSetting = JsonConvert.DeserializeObject<AudioSetting>(File.ReadAllText(settingPath));
            }
            catch
            {
                AudioSetting = new AudioSetting();
            }
        }

        public void PlayMusic(string path)
        {
            if (!audioClips.ContainsKey(path))
                audioClips.Add(path, ResLoader.Instance.LoadAsset<AudioClip>(bundleName, path));

            AudioSource.volume = AudioSetting.RealMusicVolume;
            AudioSource.clip = audioClips[path];
            AudioSource.loop = true;
            AudioSource.Play();
        }

        public void StopMusic()
        {
            AudioSource.Stop();
        }

        public void PlaySound(string path)
        {
            if (!audioClips.ContainsKey(path))
                audioClips.Add(path, ResLoader.Instance.LoadAsset<AudioClip>(bundleName, path));
            AudioSource.PlayOneShot(audioClips[path], AudioSetting.RealSoundVolume);
        }

        public void SaveSetting()
        {
            File.WriteAllText(settingPath, JsonConvert.SerializeObject(AudioSetting));
        }

        public override void Dispose()
        {
            base.Dispose();
            audioClips.Clear();
            Resources.UnloadUnusedAssets();
        }

        private void OnDestroy()
        {
            SaveSetting();
        }
    }
}