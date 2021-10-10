using System.Collections;
using System.Collections.Generic;
using Bigger;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnGUI()
    {
        if (GUILayout.Button("Open"))
            OpenTestUI();
        if (GUILayout.Button("Close"))
            CloseTestUI();
        if (GUILayout.Button("Destroy"))
            DestroyUI();
        if (GUILayout.Button("LoadScene"))
            LoadAgain();

        GUILayout.Label("当前UIcount：" + UIManager.Instance.openedPanels.Count);
    }
    public void OpenTestUI()
    {
        var panel = UIManager.Instance.GetPanel("Assets/Example/UI/UI Panel1.prefab");
        panel.Open();

    }

    public void CloseTestUI()
    {
        var panel = UIManager.Instance.GetPanel("Assets/Example/UI/UI Panel1.prefab");
        panel.Close();
    }

    public void DestroyUI()
    {
        var panel = UIManager.Instance.GetPanel("Assets/Example/UI/UI Panel1.prefab");
        Destroy(panel.gameObject);
    }

    [ContextMenu("Load Again")]
    public void LoadAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
