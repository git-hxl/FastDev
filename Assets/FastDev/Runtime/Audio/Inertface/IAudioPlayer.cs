using UnityEngine;

namespace FastDev
{
    public interface IAudioPlayer
    {
        AudioType AudioType { get; set; }
        AudioSource AudioSource { get; }
        void PlayOnOnShot(string path);
        void Play(string path);
        void Stop();
    }
}
