using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            LanguageManager.Instance.SetLanguageType(LanguageType.English);
        }

        if (Input.GetKeyUp(KeyCode.F2))
        {
            LanguageManager.Instance.SetLanguageType(LanguageType.Chinese);
        }
    }
}
