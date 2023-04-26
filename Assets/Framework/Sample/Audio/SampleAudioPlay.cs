using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleAudioPlay : MonoBehaviour
{
    public Button btPlayRandom;

    public Button btPlayUI;

    public Button btPlayMusic;
    public Button btStopMusic;

    public Slider sliderTotal;
    public Slider sliderUI;
    public Slider sliderSound;
    public Slider sliderMusic;

    // Start is called before the first frame update
    void Start()
    {
        btPlayRandom.onClick.AddListener(() =>
        {
            Vector3 pos = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            AudioManager.Instance.GetAudioPlayer(Framework.AudioType.Sound).Play("Assets/FastDev/2.AudioManager/Sample/SFX_ArrowFly.wav");

        });

        btPlayUI.onClick.AddListener(() =>
        {
            AudioManager.Instance.GetAudioPlayer(Framework.AudioType.UI).PlayOnOnShot("Assets/FastDev/2.AudioManager/Sample/UIClick.wav");
        });


        btPlayMusic.onClick.AddListener(() =>
        {
            AudioManager.Instance.GetAudioPlayer(Framework.AudioType.Sound).Play("Assets/FastDev/2.AudioManager/Sample/SFX_FireThrower_Fire_Loop.wav");
        });

        btStopMusic.onClick.AddListener(() =>
        {
            AudioManager.Instance.GetAudioPlayer(Framework.AudioType.Sound).Stop();
        });

        sliderTotal.value = AudioManager.Instance.Setting.TotalVolume;
        sliderTotal.onValueChanged.AddListener((value) =>
        {
            AudioManager.Instance.Setting.TotalVolume = value;

            AudioManager.Instance.UpdateVolume();
        });


        sliderUI.value = AudioManager.Instance.Setting.AudioTypeVolume[Framework.AudioType.UI];

        sliderUI.onValueChanged.AddListener((value) =>
        {
            AudioManager.Instance.Setting.AudioTypeVolume[Framework.AudioType.UI] = value;
            AudioManager.Instance.UpdateVolume();
        });


        sliderSound.value = AudioManager.Instance.Setting.AudioTypeVolume[Framework.AudioType.Sound];

        sliderSound.onValueChanged.AddListener((value) =>
        {
            AudioManager.Instance.Setting.AudioTypeVolume[Framework.AudioType.Sound] = value;
            AudioManager.Instance.UpdateVolume();
        });

        sliderMusic.value = AudioManager.Instance.Setting.AudioTypeVolume[Framework.AudioType.Music];

        sliderMusic.onValueChanged.AddListener((value) =>
        {
            AudioManager.Instance.Setting.AudioTypeVolume[Framework.AudioType.Music] = value;
            AudioManager.Instance.UpdateVolume();
        });
    }

    private void OnDestroy()
    {
        AudioManager.Instance?.SaveSetting();
    }
}