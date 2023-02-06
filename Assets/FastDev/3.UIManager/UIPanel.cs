using UnityEngine;
namespace FastDev
{
    public abstract class UIPanel : MonoBehaviour
    {
        public Canvas Canvas { get; private set; }

        private void Awake()
        {
            Canvas = GetComponent<Canvas>();
        }

        public virtual void Open()
        {
            gameObject.SetActive(true);
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        public void Hide()
        {
            Canvas.enabled = false;
        }

        public void UnHide()
        {
            Canvas.enabled = true;
        }
    }
}