using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bigger;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UITestPanel : UIPanel
{
    // Start is called before the first frame update
    void Start()
    {
        Button bt = transform.GetComponentInChildren<Button>();
        bt.onClick.AddListener(Close);
    }

}
