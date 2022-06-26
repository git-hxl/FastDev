namespace FastDev
{
    public class AudioSetting
    {
        private float defaultTotalVolume = 1f;
        private float defaultMusicVolume = 1f;
        private float defaultSoundVolume = 1f;

        private float totalVolume;
        public float TotalVolume { get { return totalVolume; } set { totalVolume = value; MsgManager.Instance.Dispatch(MsgID.OnVolumeChange, null); } }
        private float musicVolume;
        public float MusicVolume { get { return musicVolume; } set { musicVolume = value; MsgManager.Instance.Dispatch(MsgID.OnVolumeChange, null); } }

        private float soundVolume;
        public float SoundVolume { get { return soundVolume; } set { soundVolume = value; MsgManager.Instance.Dispatch(MsgID.OnVolumeChange, null); } }

        public AudioSetting()
        {
            totalVolume = defaultTotalVolume;
            musicVolume = defaultMusicVolume;
            soundVolume = defaultSoundVolume;
        }

        public float RealSoundVolume { get { return totalVolume * soundVolume; } }

        public float RealMusicVolume { get { return totalVolume * musicVolume; } }
    }
}
