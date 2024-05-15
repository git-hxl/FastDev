
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
            GameObject gameObject = new GameObject(soundType.ToString());
            AudioSource = gameObject.AddComponent<AudioSource>();
            this.soundManager = soundManager;
        }


        public void Play(string path)
        {
            AudioClip audioClip = GameEntry.Resource.LoadAsset<AudioClip>("audio", path);
            AudioSource.clip = audioClip;
            AudioSource.volume = soundManager.GetVolume(SoundType);
            AudioSource.Play();
        }

        public void Stop()
        {
            AudioSource.Stop();
        }

        public void Pause()
        {
            AudioSource.Pause();
        }

        public void Resume()
        {
            AudioSource.UnPause();
        }

        public void OnClear()
        {
            GameObject.Destroy(AudioSource.gameObject);
            AudioSource = null;
        }
    }
}