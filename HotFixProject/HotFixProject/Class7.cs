using Bigger;
using UnityEngine;
using UnityEngine.UI;
namespace HotFixProject
{
    class Class7 : UIPanel
    {
        public static void Add(GameObject go)
        {
            go.AddComponent<Class7>();
        }
        public Text text;
        private int times = 0;
        protected override void Start()
        {
            text = GetComponentInChildren<Text>();
            Button[] bt = GetComponentsInChildren<Button>();
            bt[0].onClick.AddListener(()=>Open());
            bt[1].onClick.AddListener(Close);
            Debug.Log("Hotfix:AddListener");
            Open();
        }


        public override void Open()
        {
            base.Open();
            text.text = "Hotfix:Open";
            times++;
            Debug.Log("Hotfix:Open:"+ times);

        }

        public override void Close()
        {
            base.Close();
            Debug.Log("Hotfix:Close");
        }
    }
}
