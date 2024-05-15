
namespace FastDev
{
    public partial class SoundManager
    {
        private class SoundSetting
        {
            public float UIVolume { get; set; }
            public float VoiceVolume { get; set; }
            public float SoundVolume { get; set; }
            public float MusicVolume { get; set; }


            public void SetVolume(SoundType type, float volume)
            {
                switch (type)
                {
                    case SoundType.UI: UIVolume = volume; break;
                    case SoundType.Voice: VoiceVolume = volume; break;
                    case SoundType.Sound: SoundVolume = volume; break;
                    case SoundType.Music: MusicVolume = volume; break;
                }
            }

            public float GetVolume(SoundType type)
            {
                switch (type)
                {
                    case SoundType.UI: return VoiceVolume;
                    case SoundType.Voice: return VoiceVolume;
                    case SoundType.Sound: return SoundVolume;
                    case SoundType.Music: return MusicVolume;

                    default: return 0f;
                }
            }

        }
    }
}
