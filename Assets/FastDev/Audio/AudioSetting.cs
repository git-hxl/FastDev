using UnityEngine;

namespace FastDev.Audio
{
    public static class AudioSetting
    {
        public static void Set2D(this IAudioPlayer audioPlayer)
        {
            audioPlayer.audioSource.spatialBlend = 0;
        }
        public static void Set3D(this IAudioPlayer audioPlayer)
        {
            audioPlayer.audioSource.spatialBlend = 1;
        }
        public static void SetMinMaxDistance(this IAudioPlayer audioPlayer,AudioRolloffMode audioRolloffMode,float minDistance,float maxDistance)
        {
            audioPlayer.audioSource.rolloffMode = audioRolloffMode;
            audioPlayer.audioSource.minDistance = minDistance;
            audioPlayer.audioSource.maxDistance = maxDistance;
        }

    }
}
