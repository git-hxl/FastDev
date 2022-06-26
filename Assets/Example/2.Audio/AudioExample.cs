using System.Collections;
using System.Collections.Generic;
using FastDev;
using UnityEngine;
using UnityEngine.UI;

public class AudioExample : MonoBehaviour
{
    public Slider slider;
    public Button bt;
    void Start()
    {
        slider.value = AudioManager.Instance.AudioSetting.TotalVolume;
       // AudioManager.Instance.PlayMusic("Assets/Example/2.Audio/textclip.mp3");

        slider.onValueChanged.AddListener((a) =>
        {
            AudioManager.Instance.AudioSetting.TotalVolume = a;
        });

        bt.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySound("Assets/Example/2.Audio/pick.mp3");
        });
    }
}
