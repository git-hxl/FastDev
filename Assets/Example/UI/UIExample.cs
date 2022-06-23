using System.Collections;
using System.Collections.Generic;
using FastDev.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button bt = GetComponentInChildren<Button>();

        bt.onClick.AddListener(()=>{
            UIManager.Instance.OpenUI("Assets/Example/UI/UITestPanel.prefab");
        }) ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
