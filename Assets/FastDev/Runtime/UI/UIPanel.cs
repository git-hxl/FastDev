using UnityEngine;
namespace FastDev
{
    public abstract class UIPanel : MonoBehaviour
    {
        public Canvas Canvas { get; protected set; }

        public virtual void OnInit()
        {
            Canvas = GetComponent<Canvas>();
        }

        public virtual void OnClose()
        {
            gameObject.SetActive(false);
        }

        public virtual void OnOpen()
        {
            gameObject.SetActive(true);
        }


        public void CloseSelf()
        {
            UIManager.Instance.CloseUI(this);
        }
    }
}