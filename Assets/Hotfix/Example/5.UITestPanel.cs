using FastDev.UI;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

namespace Hotfix
{
    public class UITestPanel : UIPanel
    {
        public override void OnClose()
        {
            transform.GetChild(0).DOScale(0, 1f).OnComplete(() => gameObject.SetActive(false)).SetId(gameObject);
        }

        public override void OnLoad(string assetPath)
        {
            Button[] buttons = transform.GetComponentsInChildren<Button>();
            buttons[0].onClick.AddListener(() =>
            {
                UIManager.instance.OpenUI(assetPath);
            });
            buttons[1].onClick.AddListener(() =>
            {
                UIManager.instance.Close(this);
            });

            TextMeshProUGUI txt = transform.GetComponentInChildren<TextMeshProUGUI>();
            txt.text = assetPath + " OnLoad";
        }

        public override void OnOpen()
        {
            gameObject.SetActive(true);
            DOTween.Kill(gameObject);
            transform.GetChild(0).DOScale(1, 1f);
        }
    }
}