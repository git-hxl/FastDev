using FastDev;
using UnityEngine;
using UnityEngine.UI;
namespace HotFixProject
{
    class Class7 : UIPanel
    {
        public Text text;
        private void Start()
        {
            text = GetComponentInChildren<Text>();
            text.text = "x";
            Button[] bt = GetComponentsInChildren<Button>();
            bt[0].onClick.AddListener(()=>Open());
            bt[1].onClick.AddListener(()=>Close());
            Debug.Log("Hotfix:AddListener");
        }


        public override void Open()
        {
            base.Open();
            Debug.Log("Hotfix:Open");

        }

        public override void Close()
        {
            base.Close();
            Debug.Log("Hotfix:Close");
        }
    }
}
