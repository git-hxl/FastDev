using UnityEngine;

namespace Framework
{
    internal interface IAudioManager
    {
        AudioClip LoadAudioClip(string path);

        AudioPlayer CreateAudioPlayer(AudioType audioType);

        AudioPlayer GetAudioPlayer(AudioType audioType);

        void RegisterAudioPlayer(AudioPlayer audioPlayer);

        void UnRegisterAudioPlayer(AudioPlayer audioSoundPlayer);
    }
}