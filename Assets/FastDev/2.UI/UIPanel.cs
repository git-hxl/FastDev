using System;
using UnityEngine;
namespace FastDev
{
    public abstract class UIPanel : MonoBehaviour, Ipanel
    {
        public virtual void OpenByAnima() { Open(); }
        public virtual void CloseByAnima() { Close(); }

        public virtual void Open()
        {
            if (!UIManager.Instance.openedPanels.Contains(this))
            {
                UIManager.Instance.openedPanels.Add(this);
                if (!gameObject.activeSelf)
                    gameObject.SetActive(true);
            }
        }

        public virtual void Close()
        {
            if (UIManager.Instance.openedPanels.Contains(this))
            {
                UIManager.Instance.openedPanels.Remove(this);
                if (gameObject.activeSelf)
                    gameObject.SetActive(false);
            }
        }
    }
}