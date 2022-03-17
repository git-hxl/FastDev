namespace FastDev
{
    public class VolumeSetting
    {
        private float defaultTotalVolume = 1f;
        private float defaultMusicVolume = 1f;
        private float defaultSoundVolume = 1f;

        public float totalVolume;
        public float musicVolume;
        public float soundVolume;

        public VolumeSetting()
        {
            totalVolume = defaultTotalVolume;
            musicVolume = defaultMusicVolume;
            soundVolume = defaultSoundVolume;
        }

        public float RealSoundVolume { get { return totalVolume * soundVolume; } }

        public float RealmusicVolume { get { return totalVolume * musicVolume; } }
    }
}