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
        private void Start()
        {
            text = GetComponentInChildren<Text>();
            Button[] bt = GetComponentsInChildren<Button>();
            bt[0].onClick.AddListener(()=>Open());
            bt[1].onClick.AddListener(this.Close);
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
