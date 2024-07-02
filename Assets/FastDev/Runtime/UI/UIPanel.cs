using UnityEngine;
namespace FastDev
{
    public abstract class UIPanel : MonoBehaviour
    {
        public string PanelName { get; protected set; }
        public Canvas Canvas { get; protected set; }

        public virtual void OnInit(string panelName)
        {
            PanelName = panelName;
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
            UIManager.Instance.CloseUI(PanelName);
        }
    }
}