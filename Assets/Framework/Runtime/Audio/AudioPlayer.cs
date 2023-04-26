using System;
using UnityEngine;

namespace Framework
{
    public class AudioPlayer : MonoBehaviour, IAudioPlayer
    {
        public AudioType AudioType;
        public AudioSource AudioSource;
        private void Awake()
        {
            if (AudioSource == null)
                AudioSource = gameObject.AddComponent<AudioSource>();
            AudioSource.volume = AudioManager.Instance.GetVolume(AudioType);
            AudioManager.Instance.RegisterAudioPlayer(this);
        }

        public void PlayOnOnShot(string path)
        {
            AudioClip clip = AudioManager.Instance.LoadAudioClip(path);
            AudioSource.PlayOneShot(clip);
        }

        public void Play(string path)
        {
            AudioClip clip = AudioManager.Instance.LoadAudioClip(path);
            AudioSource.clip = clip;
            AudioSource.Play();
        }

        public void Stop()
        {
            if (AudioSource != null)
                AudioSource.Stop();
        }

        private void OnDestroy()
        {
            AudioManager.Instance?.UnRegisterAudioPlayer(this);
        }
    }
}
