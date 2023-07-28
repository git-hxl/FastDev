using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleLanguage : MonoBehaviour
{
    public Button btEnglish;
    public Button btChinese;
    // Start is called before the first frame update
    void Start()
    {
        btEnglish.onClick.AddListener(() =>
        {
            LanguageManager.Instance.SetLanguageType(LanguageType.English);
        });

        btChinese.onClick.AddListener(() =>
        {
            LanguageManager.Instance.SetLanguageType(LanguageType.Chinese);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}