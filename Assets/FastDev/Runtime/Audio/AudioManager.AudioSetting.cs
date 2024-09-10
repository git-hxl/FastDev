
namespace FastDev
{
    public partial class AudioManager
    {
        private class AudioSetting
        {
            public float UIVolume { get; set; } = 1f;
            public float VoiceVolume { get; set; } = 1f;
            public float SoundVolume { get; set; } = 1f;
            public float MusicVolume { get; set; } = 1f;


            public void SetVolume(AudioType type, float volume)
            {
                switch (type)
                {
                    case AudioType.UI: UIVolume = volume; break;
                    case AudioType.Voice: VoiceVolume = volume; break;
                    case AudioType.Sound: SoundVolume = volume; break;
                    case AudioType.Music: MusicVolume = volume; break;
                }
            }

            public float GetVolume(AudioType type)
            {
                switch (type)
                {
                    case AudioType.UI: return UIVolume;
                    case AudioType.Voice: return VoiceVolume;
                    case AudioType.Sound: return SoundVolume;
                    case AudioType.Music: return MusicVolume;

                    default: return 0f;
                }
            }

        }
    }
}
