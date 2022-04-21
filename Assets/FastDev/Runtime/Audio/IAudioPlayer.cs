using System;
using UnityEngine;
namespace FastDev.Audio
{
    public interface IAudioPlayer
    {
        AudioType audioType { get; }
        AudioSource audioSource { get; }

        event Action OnPlayEnd;

        void Play();

        void Play(float duration);

        void Play(Vector3 Position);

        void Pause();
        void Pause(float duration);

        void Stop();
        void Stop(float duration);

        void Dispose();
    }
}
