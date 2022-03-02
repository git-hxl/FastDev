using FastDev;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace HotFixProject
{
    class Class7 : UIBase
    {
        public Text text;
        private void Start()
        {
            text = GetComponentInChildren<Text>();
            text.text = "x";
            Button[] bt = GetComponentsInChildren<Button>();
            bt[1].onClick.AddListener(Close);
            bt[0].onClick.AddListener(Open);
            
            Debug.Log("Hotfix:AddListener");
        }

        protected override Sequence OpenAnima()
        {
            return null;
        }

        protected override Sequence CloseAnima()
        {
            return null;
        }
    }
}
