using System;
using UnityEngine;
namespace Bigger
{
    public abstract class UIPanel : MonoBehaviour, Ipanel
    {
        public virtual void PlayAnimaOnOpen(Action Active) { Active?.Invoke(); }
        public virtual void PlayAnimaOnClose(Action Inactive) { Inactive?.Invoke(); }

        public virtual void Open()
        {
            if (!UIManager.Instance.openedPanels.Contains(this))
            {
                PlayAnimaOnOpen(() => gameObject.SetActive(true));
                UIManager.Instance.openedPanels.Add(this);
            }
        }

        public virtual void Close()
        {
            if (UIManager.Instance.openedPanels.Contains(this))
            {
                PlayAnimaOnClose(() => gameObject.SetActive(false));
                UIManager.Instance.openedPanels.Remove(this);
            }
        }
    }
}