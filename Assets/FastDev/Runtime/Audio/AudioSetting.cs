using System;
using System.Collections.Generic;

namespace FastDev
{
    public class AudioSetting
    {
        public bool OpenTotalVolume = true;
        public bool OpenMusicVolume = true;

        public float TotalVolume = 1f;

        public Dictionary<AudioType, float> AudioTypeVolume;
        public AudioSetting()
        {
            AudioTypeVolume = new Dictionary<AudioType, float>();

            foreach (AudioType item in Enum.GetValues(typeof(AudioType)))
            {
                AudioTypeVolume.Add(item, 1f);
            }
        }
    }
}