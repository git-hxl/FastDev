
using FastDev;
using UnityEngine;
using UnityEngine.UI;

public class SampleUI : MonoBehaviour
{
    public Button btOpen;
    public Button btOpen2;
    // Start is called before the first frame update
    void Start()
    {
        btOpen.onClick.AddListener(() =>
        {
            var uIPanel = UIManager.Instance.OpenUI("Assets/FastDev/Sample/UI/SampleUIPanel.prefab");
        });

        btOpen2.onClick.AddListener(() =>
        {
            var uIPanel = UIManager.Instance.OpenUI("Assets/FastDev/Sample/UI/SampleUIPanel2.prefab");
        });
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    UIPanel uIPanel = UIManager.Instance.GetTopActiveUI();
        //    if (uIPanel != null)
        //    {
        //        uIPanel.CloseUI();
        //    }

        //}

        //if (Input.GetKeyDown(KeyCode.F1))
        //{
        //    UIManager.Instance.HideAllActiveUI();
        //}
        //if (Input.GetKeyUp(KeyCode.F1))
        //{
        //    UIManager.Instance.ShowAllHidedUI();
        //}
    }
}