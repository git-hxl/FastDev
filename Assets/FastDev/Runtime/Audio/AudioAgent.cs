
using System.IO;
using UnityEngine;

namespace FastDev
{
    public class AudioAgent : IReference
    {
        public string AudioPath { get; private set; }
        public AudioType AudioType { get; private set; }
        public AudioSource AudioSource { get; private set; }
        public AudioAgent()
        {

        }

        public void Init(AudioType audioType)
        {
            AudioType = audioType;
            CheckAudioSource();
        }

        private void CheckAudioSource()
        {
            if (AudioSource == null)
            {
                GameObject gameObject = new GameObject($"{AudioType} AudioSource");
                AudioSource = gameObject.AddComponent<AudioSource>();
                AudioSource.loop = AudioType == AudioType.Music;

                AudioSource.volume = AudioManager.Instance.GetVolume(AudioType);
            }
        }

        public void Play(string path)
        {
            CheckAudioSource();

            string soundName = Path.GetFileNameWithoutExtension(path);

            if (AudioSource.clip == null || AudioSource.clip.name != soundName)
            {
                AudioClip audioClip = ResourceManager.Instance.LoadAsset<AudioClip>("audio", path);
                AudioSource.clip = audioClip;
            }
            AudioSource.Play();
        }

        public void PlayOneShot(string path)
        {
            CheckAudioSource();
            AudioClip audioClip = ResourceManager.Instance.LoadAsset<AudioClip>("audio", path);
            AudioSource.PlayOneShot(audioClip);
        }

        public void Stop()
        {
            if (AudioSource == null)
                return;

            AudioSource.Stop();
        }

        public void Pause()
        {
            if (AudioSource == null)
                return;
            AudioSource.Pause();
        }

        public void Resume()
        {
            if (AudioSource == null)
                return;
            AudioSource.UnPause();
        }

        public void OnClear()
        {
            GameObject.Destroy(AudioSource.gameObject);
            AudioSource = null;
        }
    }
}