namespace FastDev.Audio
{
    public class AudioSetting
    {
        public float volume;
        public float spatialBlend;
        public float minDistance;
        public float maxDistance;
        public bool loop;
        public static AudioSetting Default
        {
            get
            {
                return new AudioSetting()
                {
                    volume = AudioManager.instance.volumeSetting.RealSoundVolume,
                    loop = false,
                    spatialBlend = 0,
                };
            }
        }

        public static AudioSetting Music
        {
            get
            {
                return new AudioSetting()
                {
                    volume = AudioManager.instance.volumeSetting.RealmusicVolume,
                    loop = true,
                    spatialBlend = 0,
                };
            }
        }

        public static AudioSetting Bullet
        {
            get
            {
                return new AudioSetting()
                {
                    volume = AudioManager.instance.volumeSetting.RealSoundVolume,
                    loop = false,
                    spatialBlend = 1,
                };
            }
        }

    }
}
