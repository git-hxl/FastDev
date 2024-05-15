
using System.Collections.Generic;

namespace FastDev
{
    public sealed partial class SoundManager : GameModule
    {
        private Dictionary<SoundType, SoundAgent> soundAgents;

        private SoundSetting soundSetting;


        public SoundManager()
        {
            soundAgents = new Dictionary<SoundType, SoundAgent>();
        }

        /// <summary>
        /// 获取声音代理
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public SoundAgent GetSoundAgent(SoundType type)
        {
            if (!soundAgents.ContainsKey(type))
            {
                SoundAgent soundAgent = ReferencePool.Acquire<SoundAgent>();
                soundAgent.Init(type, this);

                soundAgents.Add(type, soundAgent);
            }

            return soundAgents[type];
        }

        /// <summary>
        /// 获取音量大小
        /// </summary>
        /// <param name="soundType"></param>
        /// <returns></returns>
        public float GetVolume(SoundType soundType)
        {
            return soundSetting.GetVolume(soundType);
        }

        /// <summary>
        /// 设置音量大小
        /// </summary>
        /// <param name="soundType"></param>
        /// <param name="value"></param>
        public void SetVolume(SoundType soundType, float value)
        {
            soundSetting.SetVolume(soundType, value);

            foreach (var item in soundAgents)
            {
                if (item.Value.AudioSource.isPlaying)
                {
                    item.Value.AudioSource.volume = value;
                }
            }
        }


        internal override void Shutdown()
        {

            foreach (var item in soundAgents)
            {
                ReferencePool.Release(item.Value);
            }

            soundAgents.Clear();
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            //throw new System.NotImplementedException();
        }
    }
}
