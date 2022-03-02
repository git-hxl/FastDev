using System;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
namespace FastDev
{
    public abstract class UIBase : MonoBehaviour
    {
        protected virtual Sequence OpenAnima()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.GetChild(0).DOScale(1.25f, 0.25f));
            sequence.Append(transform.GetChild(0).DOScale(1, 0.15f));
            sequence.Play();
            return sequence;
        }
        protected virtual Sequence CloseAnima()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.GetChild(0).DOScale(1.25f, 0.25f));
            sequence.Append(transform.GetChild(0).DOScale(0, 0.15f));
            sequence.Play();
            return sequence;
        }

        public virtual void Open()
        {
            if (!UIManager.Instance.openedPanels.Contains(this))
            {
                UIManager.Instance.openedPanels.Add(this);
                if (!gameObject.activeSelf)
                    gameObject.SetActive(true);
                OpenAnima();
            }
            Debug.Log("Unity UIBase Open");
        }

        public async virtual void Close()
        {
            Sequence sequence = CloseAnima();
            if (sequence!=null)
                await sequence.AwaitForComplete();
            if (UIManager.Instance.openedPanels.Contains(this))
            {
                UIManager.Instance.openedPanels.Remove(this);
                if (gameObject.activeSelf)
                    gameObject.SetActive(false);
            }
            Debug.Log("Unity UIBase Close");
        }
    }
}