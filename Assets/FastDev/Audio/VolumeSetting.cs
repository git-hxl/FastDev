namespace FastDev
{
    public class VolumeSetting
    {
        private float defaultTotalVolume = 1f;
        private float defaultMusicVolume = 1f;
        private float defaultSoundVolume = 1f;

        private float _totalVolume;
        public float totalVolume { get { return _totalVolume; } set { _totalVolume = value; MsgManager.instance.Dispatch(MsgID.OnVolumeChange, null); } }
        private float _musicVolume;
        public float musicVolume { get { return _musicVolume; } set { _musicVolume = value; MsgManager.instance.Dispatch(MsgID.OnVolumeChange, null); } }
        private float _soundVolume;
        public float soundVolume { get { return _soundVolume; } set { _soundVolume = value; MsgManager.instance.Dispatch(MsgID.OnVolumeChange, null); } }

        public VolumeSetting()
        {
            totalVolume = defaultTotalVolume;
            musicVolume = defaultMusicVolume;
            soundVolume = defaultSoundVolume;
        }

        public float RealSoundVolume { get { return totalVolume * soundVolume; } }

        public float RealMusicVolume { get { return totalVolume * musicVolume; } }
    }
}