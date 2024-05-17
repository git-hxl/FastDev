
using System.IO;
using UnityEngine;

namespace FastDev
{
    public class SoundAgent : IReference
    {
        public string SoundPath { get; private set; }
        public SoundType SoundType { get; private set; }
        public AudioSource AudioSource { get; private set; }

        private SoundManager soundManager;

        public SoundAgent()
        {

        }

        public void Init(SoundType soundType, SoundManager soundManager)
        {
            SoundType = soundType;
            this.soundManager = soundManager;

            CheckAudioSource();
        }

        private void CheckAudioSource()
        {
            if (AudioSource == null)
            {
                GameObject gameObject = new GameObject($"{SoundType} AudioSource");
                AudioSource = gameObject.AddComponent<AudioSource>();
                AudioSource.loop = SoundType == SoundType.Music;

                AudioSource.volume = soundManager.GetVolume(SoundType);
            }
        }

        public void Play(string path)
        {
            CheckAudioSource();

            string soundName = Path.GetFileNameWithoutExtension(path);

            if (AudioSource.clip == null || AudioSource.clip.name != soundName)
            {
                AudioClip audioClip = GameEntry.Resource.LoadAsset<AudioClip>("audio", path);
                AudioSource.clip = audioClip;
            }
            AudioSource.Play();
        }

        public void PlayOneShot(string path)
        {
            CheckAudioSource();
            AudioClip audioClip = GameEntry.Resource.LoadAsset<AudioClip>("audio", path);
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