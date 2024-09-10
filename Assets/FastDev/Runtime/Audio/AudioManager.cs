
using System.Collections.Generic;

namespace FastDev
{
    public sealed partial class AudioManager : MonoSingleton<AudioManager>
    {
        private Dictionary<AudioType, AudioAgent> audioAgents;

        private AudioSetting audioSetting;


        public AudioManager()
        {
            audioAgents = new Dictionary<AudioType, AudioAgent>();
            audioSetting = new AudioSetting();
        }

        /// <summary>
        /// 获取声音代理
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public AudioAgent GetAudioAgent(AudioType type)
        {
            if (!audioAgents.ContainsKey(type))
            {
                AudioAgent audioAgent = ReferencePool.Acquire<AudioAgent>();
                audioAgent.Init(type);

                audioAgents.Add(type, audioAgent);
            }

            return audioAgents[type];
        }

        /// <summary>
        /// 获取音量大小
        /// </summary>
        /// <param name="soundType"></param>
        /// <returns></returns>
        public float GetVolume(AudioType soundType)
        {
            return audioSetting.GetVolume(soundType);
        }

        /// <summary>
        /// 设置音量大小
        /// </summary>
        /// <param name="soundType"></param>
        /// <param name="value"></param>
        public void SetVolume(AudioType soundType, float value)
        {
            audioSetting.SetVolume(soundType, value);

            foreach (var item in audioAgents)
            {
                if (item.Value.AudioSource.isPlaying)
                {
                    item.Value.AudioSource.volume = value;
                }
            }
        }


        protected override void OnDispose()
        {
            base.OnDispose();
            foreach (var item in audioAgents)
            {
                ReferencePool.Release(item.Value);
            }

            audioAgents.Clear();
        }
    }
}
