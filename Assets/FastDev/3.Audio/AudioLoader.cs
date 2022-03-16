using UnityEngine;
namespace FastDev
{
    public class AudioLoader : MonoBehaviour
    {
        public AudioType audioType;
        public string path;
        private void Start()
        {
            if (audioType == AudioType.Sound)
            {
                AudioManager.instance.PlayClip(path);
            }
            else if (audioType == AudioType.Music)
            {
                AudioManager.instance.PlayMusic(path);
            }
        }
    }
}