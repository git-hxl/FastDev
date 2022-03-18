namespace FastDev.Audio
{
    public enum AudioType
    {
        Sound,
        Music,
    }
    public class AudioSetting
    {
        public AudioType audioType;
        public float spatialBlend;
        public float minDistance;
        public float maxDistance;
        public bool loop;
        public AudioSetting()
        {
            loop = false;
            spatialBlend = 0;
            audioType = AudioType.Sound;
        }

        public static AudioSetting Default => new AudioSetting()
        {
        };

        public static AudioSetting Music => new AudioSetting()
        {
            loop = true,
            spatialBlend = 0,
            audioType = AudioType.Music,
        };

        public static AudioSetting Bullet => new AudioSetting()
        {
            loop = false,
            spatialBlend = 1,
        };

    }
}
