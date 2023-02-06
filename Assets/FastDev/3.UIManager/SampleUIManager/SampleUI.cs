using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FastDev
{
    public class SampleUI : MonoBehaviour
    {
        public Button btOpen;
        public Button btOpen2;
        // Start is called before the first frame update
        void Start()
        {
            btOpen.onClick.AddListener(() =>
            {
                UIManager.Instance.OpenUI("Assets/FastDev/3.UIManager/SampleUIManager/SampleUIPanel.prefab");
            });

            btOpen2.onClick.AddListener(() =>
            {
                UIManager.Instance.OpenUI("Assets/FastDev/3.UIManager/SampleUIManager/SampleUIPanel2.prefab");
            });
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.Instance.transform.GetComponentInChildren<UIPanel>()?.Close();
            }

            if (Input.GetKeyDown(KeyCode.F1))
            {
                UIManager.Instance.HideAllActiveUI();
            }
            if (Input.GetKeyUp(KeyCode.F1))
            {
                UIManager.Instance.ShowAllHidedUI();
            }
        }
    }
}
