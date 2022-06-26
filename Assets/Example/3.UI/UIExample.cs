using System.Collections;
using System.Collections.Generic;
using FastDev;
using UnityEngine;
using UnityEngine.UI;

public class UIExample : MonoBehaviour
{
    public string uiPath;
    // Start is called before the first frame update
    void Start()
    {
        Button bt = GetComponentInChildren<Button>();

        bt.onClick.AddListener(()=>{
            UIManager.Instance.LoadPanel(uiPath).Open();
        }) ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
