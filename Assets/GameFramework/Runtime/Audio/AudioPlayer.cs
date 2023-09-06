using System;
using UnityEngine;

namespace GameFramework
{
    public class AudioPlayer : MonoBehaviour, IAudioPlayer
    {
        [SerializeField]
        private AudioType audioType;
        public AudioType AudioType { get => audioType; set => audioType = value; }
        public AudioSource AudioSource { get; private set; }

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
            if (AudioSource == null)
            {
                AudioSource = gameObject.AddComponent<AudioSource>();
            }
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
            if (AudioSource != null)
            {
                Destroy(AudioSource);
            }
            AudioManager.Instance?.UnRegisterAudioPlayer(this);
        }
    }
}
