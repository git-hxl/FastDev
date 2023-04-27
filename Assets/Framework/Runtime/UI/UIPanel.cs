using Framework;
using UnityEngine;
namespace Framework
{
    public abstract class UIPanel : MonoBehaviour, IUIPanel
    {
        public Canvas Canvas { get; protected set; }
        private void Awake()
        {
            Canvas = GetComponent<Canvas>();
        }

        public virtual IUIPanel Open()
        {
            gameObject.SetActive(true);
            return this;
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }
    }
}