using System.Collections;
using System.Collections.Generic;
using FastDev;
using FastDev.Audio;
using UnityEngine;
using UnityEngine.UI;

public class AudioExample : MonoBehaviour
{
    public string clipPath;
    public string clipPath2;

    AudioPlayer audioPlayer1;
    AudioPlayer audioPlayer2;
    AudioPlayer audioPlayer3;
    void Start()
    {
        Button[] buttons = GetComponentsInChildren<Button>();

        audioPlayer1 = new AudioPlayer(FastDev.Audio.AudioType.Music, clipPath);
        audioPlayer2 = new AudioPlayer(FastDev.Audio.AudioType.Music, clipPath2);

        buttons[0].onClick.AddListener(() =>
        {
            audioPlayer1.Play(1f);
            audioPlayer2.Pause(1f);
        });

        buttons[1].onClick.AddListener(() =>
        {
            audioPlayer1.Pause(1f);
        });

        buttons[2].onClick.AddListener(() =>
        {
            audioPlayer1.Stop();
        });


        buttons[3].onClick.AddListener(() =>
        {
            audioPlayer1.Pause(1f);
            audioPlayer2.Play(1f);
        });

        buttons[4].onClick.AddListener(() =>
        {
            audioPlayer2.Pause(1f);
        });

        buttons[5].onClick.AddListener(() =>
        {
            audioPlayer2.Stop();
        });


        buttons[6].onClick.AddListener(() =>
        {
            var audioPlayer3 = new AudioPlayer(FastDev.Audio.AudioType.Sound, "Assets/Example/Res/shot.mp3");
            audioPlayer3.Set3D();
            audioPlayer3.SetMinMaxDistance(AudioRolloffMode.Linear, 0, 50);
            audioPlayer3.Play(new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)));
            audioPlayer3.OnPlayEnd += audioPlayer3.Dispose;
        });


        Slider[] slider = GetComponentsInChildren<Slider>();
        slider[0].onValueChanged.AddListener((value) => AudioManager.instance.volumeSetting.totalVolume = value);
        slider[1].onValueChanged.AddListener((value) => AudioManager.instance.volumeSetting.soundVolume = value);

        slider[2].onValueChanged.AddListener((value) => AudioManager.instance.volumeSetting.musicVolume = value);
    }

    private void OnDestroy()
    {
    }
}
