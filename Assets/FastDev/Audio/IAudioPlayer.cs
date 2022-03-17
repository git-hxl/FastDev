using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace FastDev.Audio
{
    public interface IAudioPlayer
    {
        event Action<string> OnPlayEnd;

        void PlayClip(string clipPath);

        void PlayClip(string cliPath, float duration);

        void PlayClip(string clipPath, Vector3 Position);

        void Stop(string clipPath);
        void Stop(string clipPath, float duration);

        void Pause(string clipPath);
        void Pause(string clipPath, float duration);

        void Dispose(string clipPath);

        void DisposeAll();
    }
}
