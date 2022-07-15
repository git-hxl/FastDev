using UnityEngine;
namespace FastDev
{
    public abstract class UIPanel : MonoBehaviour, IPanel
    {
        public void Close()
        {
            UIManager.Instance.Close(this);
        }
        public void Open()
        {
            UIManager.Instance.Open(this);
        }
        public abstract void OnClose();
        public abstract void OnLoad();
        public abstract void OnOpen();
    }
}