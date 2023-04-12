using UnityEngine;
namespace FastDev
{
    public abstract class UIPanel : MonoBehaviour
    {
        public Canvas Canvas { get; protected set; }
        private void Awake()
        {
            Canvas = GetComponent<Canvas>();
        }
        public virtual void OnOpen()
        {
            gameObject.SetActive(true);
        }
        public virtual void OnClose()
        {
            gameObject.SetActive(false);
        }
    }
}