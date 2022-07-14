using UnityEngine;
namespace FastDev
{
    public abstract class UIPanel : MonoBehaviour, IPanel
    {
        public void Close()
        {
            var peekPanel = UIManager.Instance.OpenedPanels.Peek() as UIPanel;
            if (peekPanel != this)
            {
                Debug.LogError("close ui failed,is not curOpened ui");
                return;
            }
            UIManager.Instance.OpenedPanels.Pop();
            OnClose();
        }
        public void Load()
        {

            OnLoad();
        }
        public void Open()
        {
            UIManager.Instance.OpenedPanels.Push(this);
            OnOpen();
        }

        protected abstract void OnClose();
        protected abstract void OnLoad();
        protected abstract void OnOpen();
    }
}