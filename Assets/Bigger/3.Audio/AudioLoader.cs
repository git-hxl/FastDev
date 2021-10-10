using UnityEngine;
namespace Bigger
{
    public class AudioLoader : MonoBehaviour
    {
        public AudioType audioType;
        public string path;
        private void Start()
        {
            if (audioType == AudioType.Sound)
            {
                AudioManager.Instance.PlayClip(path);
            }
            else if (audioType == AudioType.Music)
            {
                AudioManager.Instance.PlayMusic(path);
            }
        }
    }
}