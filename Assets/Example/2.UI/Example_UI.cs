using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastDev;
public class Example_UI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.GetPanel("Assets/Example/2.UI/UIPanel1.prefab").Open();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
