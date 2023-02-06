using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace FastDev
{
    public class AudioPlayer
    {
        public AudioSource AudioSource { get; private set; }
        public AudioType AudioType { get; private set; }

        private CancellationTokenSource playToken;
        private CancellationTokenSource stopToken;
        public AudioPlayer(AudioType audioType, GameObject target)
        {
            AudioType = audioType;
            AudioSource = target.AddComponent<AudioSource>();
            switch (audioType)
            {
                case AudioType.Music:
                    AudioSource.loop = true;
                    break;
            }
            AudioSource.volume = AudioManager.Instance.GetVolume(audioType);
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

        public void PlaySmooth(string path)
        {
            playToken = new CancellationTokenSource();
            if (stopToken != null)
            {
                stopToken.Cancel();
            }

            Play(path);

            float targetVolume = AudioManager.Instance.GetVolume(AudioType);
            AudioSource.volume = 0;
            UniTask.Create(async () =>
            {
                while (AudioSource != null && AudioSource.volume < targetVolume)
                {
                    AudioSource.volume += Time.deltaTime;
                    await UniTask.DelayFrame(1, cancellationToken: playToken.Token);
                }
            }).Forget();
        }

        public void Stop()
        {
            if (AudioSource != null)
                AudioSource.Stop();
        }

        public void StopSmooth()
        {
            stopToken = new CancellationTokenSource();
            if (playToken != null)
            {
                playToken.Cancel();
            }
                
            if (AudioSource != null && AudioSource.isPlaying)
            {
                UniTask.Create(async () =>
                {
                    while (AudioSource != null && AudioSource.volume > 0)
                    {
                        AudioSource.volume -= Time.deltaTime;
                        await UniTask.DelayFrame(1, cancellationToken: stopToken.Token);
                    }
                    Stop();
                }).Forget();
            }
        }
    }
}
