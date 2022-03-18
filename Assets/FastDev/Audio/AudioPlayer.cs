using Cysharp.Threading.Tasks;
using FastDev.Res;
using System;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

namespace FastDev.Audio
{
    public class AudioPlayer : IAudioPlayer
    {
        private AudioSetting audioSetting;

        public event Action<string> OnPlayEnd;

        private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();

        public AudioPlayer(AudioSetting audioSetting)
        {
            this.audioSetting = audioSetting;

            MsgManager.instance.Register(MsgID.OnVolumeChange, OnVolumeChange);
        }

        public async void PlayClip(string clipPath)
        {
            var audioSource = GetAudioSource(clipPath);
            audioSource.clip = GetAudioClip(clipPath);
            audioSource.Play();
            if (audioSource.loop == false)
            {
                await UniTask.WaitUntil(() => audioSource.time == audioSource.clip.length);
                Stop(clipPath);
            }
        }

        public void PlayClip(string clipPath, float duration)
        {
            var audioSource = GetAudioSource(clipPath);
            audioSource.volume = 0;
            audioSource.DOFade(GetAudioVolume(), duration);
            PlayClip(clipPath);
        }

        public void PlayClip(string clipPath, Vector3 Position)
        {
            var audioSource = GetAudioSource(clipPath);
            audioSource.transform.position = Position;
            PlayClip(clipPath);
        }

        public void Stop(string clipPath)
        {
            var audioSource = GetAudioSource(clipPath);
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Stop();
                audioSource.clip = null;
                OnPlayEnd?.Invoke(clipPath);
            }
        }
        public void Stop(string clipPath, float duration)
        {
            var audioSource = GetAudioSource(clipPath);
            if (audioSource != null)
                audioSource.DOFade(0, duration).OnComplete(() => Stop(clipPath));
        }

        public void Pause(string clipPath)
        {
            var audioSource = GetAudioSource(clipPath);
            if (audioSource != null)
                audioSource.Pause();
        }
        public void Pause(string clipPath, float duration)
        {
            var audioSource = GetAudioSource(clipPath);
            if (audioSource != null)
                audioSource.DOFade(0, duration).OnComplete(() => Pause(clipPath));
        }

        public void Dispose(string clipPath)
        {
            var audioSource = GetAudioSource(clipPath);
            if (audioSource != null)
            {
                UnityEngine.Object.Destroy(audioSource.gameObject);
                audioSources.Remove(clipPath);
            }
        }

        public void DisposeAll()
        {
            foreach (var item in audioSources)
            {
                Dispose(item.Key);
            }
        }

        private void OnVolumeChange(Hashtable hashtable)
        {
            foreach (var item in audioSources)
            {
                if (item.Value != null)
                {
                    item.Value.volume = GetAudioVolume();
                }
            }
        }

        private float GetAudioVolume()
        {
            float volume = 0;
            switch (audioSetting.audioType)
            {
                case AudioType.Sound:
                    volume =  AudioManager.instance.volumeSetting.RealSoundVolume;
                    break;
                case AudioType.Music:
                    volume =  AudioManager.instance.volumeSetting.RealMusicVolume;
                    break;
            }
            return volume;
        }

        private AudioSource GetAudioSource(string clipPath)
        {
            if (!audioSources.ContainsKey(clipPath) || audioSources[clipPath] == null)
            {
                GameObject obj = new GameObject("Audio Player");
                var audioSource = obj.AddComponent<AudioSource>();
                audioSource.volume = GetAudioVolume();
                audioSource.spatialBlend = audioSetting.spatialBlend;
                audioSource.minDistance = audioSetting.minDistance;
                audioSource.maxDistance = audioSetting.maxDistance;
                audioSource.loop = audioSetting.loop;
                audioSource.playOnAwake = false;
                audioSources[clipPath] = audioSource;
            }
            return audioSources[clipPath];
        }
        private AudioClip GetAudioClip(string assetPath)
        {
            AudioClip audioClip = null;
            audioClip = ResManager.instance.LoadAsset<AudioClip>(ResConstant.audio, assetPath);
            return audioClip;
        }
    }
}