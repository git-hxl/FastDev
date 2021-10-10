using UnityEngine;
namespace Bigger
{
    public abstract class UIPanel : MonoBehaviour, Ipanel
    {
        public virtual void PlayAnimaOnOpen() { }
        public virtual void PlayAnimaOnClose() { }

        public virtual void Open()
        {
            if (!UIManager.Instance.openedPanels.Contains(this))
            {
                PlayAnimaOnOpen();
                UIManager.Instance.openedPanels.Add(this);
                gameObject.SetActive(true);
            }
        }

        public virtual void Close()
        {
            if (UIManager.Instance.openedPanels.Contains(this))
            {
                PlayAnimaOnClose();
                UIManager.Instance.openedPanels.Remove(this);
                gameObject.SetActive(false);
            }
        }
    }
}