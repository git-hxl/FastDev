using FastDev;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using FastDev.UI;

namespace HotFixProject
{
    class Class7 : UIPanel
    {
        public Text text;


        public override void OnClose()
        {
            gameObject.SetActive(false);
            Debug.Log("OnClose");
        }


        public override void OnOpen()
        {
            gameObject.SetActive(true);
            Debug.Log("OnOpen");
        }

        public override void OnLoad(string assetPath)
        {
            text = GetComponentInChildren<Text>();
            text.text = "x";
            Button[] bt = GetComponentsInChildren<Button>();
            bt[1].onClick.AddListener(() => UIManager.instance.Close(this));
            bt[0].onClick.AddListener(OnOpen);

            Debug.Log("Hotfix:AddListener");
        }
    }
}
