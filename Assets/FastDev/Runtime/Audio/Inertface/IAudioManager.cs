using UnityEngine;

namespace FastDev
{
    internal interface IAudioManager
    {
        AudioClip LoadAudioClip(string path);

        IAudioPlayer CreateAudioPlayer(AudioType audioType);

        IAudioPlayer GetAudioPlayer(AudioType audioType);

        void RegisterAudioPlayer(IAudioPlayer audioPlayer);

        void UnRegisterAudioPlayer(IAudioPlayer audioSoundPlayer);
    }
}