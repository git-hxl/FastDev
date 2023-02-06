using Newtonsoft.Json;
using System.IO;
using UnityEngine;

namespace FastDev
{
    public class AudioManagerSetting
    {
        public float TotalVolume { get; set; } = 1f;

        public float UIVolume { get; set; } = 1f;

        public float SoundVolume { get; set; } = 1f;

        public float MusicVolume { get; set; } = 1f;

        public void Save()
        {
            string path = Application.streamingAssetsPath + "/AudioSetting.json";
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public static AudioManagerSetting Init()
        {
            string path = Application.streamingAssetsPath + "/AudioSetting.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<AudioManagerSetting>(json);
            }
            return new AudioManagerSetting();
        }
    }
}