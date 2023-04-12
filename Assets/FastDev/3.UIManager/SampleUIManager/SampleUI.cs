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
                UIPanel uIPanel = UIManager.Instance.LoadUIPanel("Assets/FastDev/3.UIManager/SampleUIManager/SampleUIPanel.prefab");
                uIPanel.OpenUI();
            });

            btOpen2.onClick.AddListener(() =>
            {
                UIPanel uIPanel = UIManager.Instance.LoadUIPanel("Assets/FastDev/3.UIManager/SampleUIManager/SampleUIPanel2.prefab");
                uIPanel.OpenUI();
            });
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIPanel uIPanel = UIManager.Instance.GetTopActiveUI();
                if (uIPanel != null)
                {
                    uIPanel.CloseUI();
                }

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
