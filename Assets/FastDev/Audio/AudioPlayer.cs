using Cysharp.Threading.Tasks;
using FastDev.Res;
using System;
using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace FastDev.Audio
{
    public class AudioPlayer : IAudioPlayer
    {
        private AudioType _audioType;

        public AudioType audioType { get { return _audioType; } }

        public event Action OnPlayEnd;

        private AudioSource _audioSource;

        public AudioSource audioSource { get { return _audioSource; } }

        public AudioPlayer(AudioType audioType, string clipPath)
        {
            this._audioType = audioType;
            if (_audioSource == null)
            {
                GameObject obj = new GameObject("Audio Player");
                _audioSource = obj.AddComponent<AudioSource>();
                _audioSource.volume = GetAudioVolume();
                _audioSource.playOnAwake = false;
                _audioSource.loop = audioType == AudioType.Music;
                _audioSource.clip = GetAudioClip(clipPath);
            }
            MsgManager.instance.Register(MsgID.OnVolumeChange, OnVolumeChange);
        }

        public async void Play()
        {
            _audioSource.Play();
            if (_audioSource.loop == false)
            {
                await UniTask.WaitUntil(() => _audioSource == null || (_audioSource.time == _audioSource.clip.length));
                Stop();
            }
        }

        public void Play(float duration)
        {
            DOTween.To(() => 0, (value) => { if (_audioSource != null) _audioSource.volume = value; }, GetAudioVolume(), duration);
            Play();
        }

        public void Play(Vector3 Position)
        {
            _audioSource.transform.position = Position;
            Play();
        }

        public void Pause()
        {
            if (_audioSource != null)
                _audioSource.Pause();
        }

        public void Pause(float duration)
        {
            if (_audioSource != null)
            {
                float volume = _audioSource.volume;
                DOTween.To(() => volume, (value) => { if (_audioSource != null) _audioSource.volume = value; }, 0, duration).OnComplete(Pause);
            }
        }

        public void Stop()
        {
            if (_audioSource != null)
            {
                _audioSource.Stop();
                OnPlayEnd?.Invoke();
            }
        }
        public void Stop(float duration)
        {
            if (_audioSource != null)
            {
                float volume = _audioSource.volume;
                DOTween.To(() => volume, (value) => { if (_audioSource != null) _audioSource.volume = value; }, 0, duration).OnComplete(Stop);
            }
        }

        public void Dispose()
        {
            if (_audioSource != null)
            {
                _audioSource.clip = null;
                UnityEngine.Object.Destroy(_audioSource.gameObject);
                _audioSource = null;
                MsgManager.instance.UnRegister(MsgID.OnVolumeChange, OnVolumeChange);
            }
        }

        private void OnVolumeChange(Hashtable hashtable)
        {
            _audioSource.volume = GetAudioVolume();
        }

        private float GetAudioVolume()
        {
            float volume = 0;
            switch (audioType)
            {
                case AudioType.Sound:
                    volume = AudioManager.instance.volumeSetting.RealSoundVolume;
                    break;
                case AudioType.Music:
                    volume = AudioManager.instance.volumeSetting.RealMusicVolume;
                    break;
            }
            return volume;
        }

        private AudioClip GetAudioClip(string assetPath)
        {
            AudioClip audioClip = null;
            audioClip = ResManager.instance.LoadAsset<AudioClip>(ResConstant.audio, assetPath);
            return audioClip;
        }
    }
}