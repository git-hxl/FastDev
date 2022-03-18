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
    IAudioPlayer audioPlayer;
    void Start()
    {
        Button[] buttons = GetComponentsInChildren<Button>();

        if (audioPlayer == null)
            audioPlayer = AudioManager.instance.GetMusicAudioPlayer();

        buttons[0].onClick.AddListener(() =>
        {
            audioPlayer.PlayClip(clipPath, 1f);
            audioPlayer.Pause(clipPath2, 1f);
        });

        buttons[1].onClick.AddListener(() =>
        {
            audioPlayer.Pause(clipPath, 1f);
        });

        buttons[2].onClick.AddListener(() =>
        {
            audioPlayer.Stop(clipPath);
        });


        buttons[3].onClick.AddListener(() =>
        {
            audioPlayer.Pause(clipPath, 1f);
            audioPlayer.PlayClip(clipPath2, 1f);
        });

        buttons[4].onClick.AddListener(() =>
        {
            audioPlayer.Pause(clipPath2, 1f);
        });

        buttons[5].onClick.AddListener(() =>
        {
            audioPlayer.Stop(clipPath2);
        });


        buttons[6].onClick.AddListener(() =>
        {
            var audioPlayer = AudioManager.instance.GetDefaultAudioPlayer();
            audioPlayer.PlayClip("Assets/Example/Res/shot.mp3");

            audioPlayer.OnPlayEnd += (clipPath) => audioPlayer.Dispose(clipPath);
        });


        Slider[] slider = GetComponentsInChildren<Slider>();
        slider[0].onValueChanged.AddListener((value) => AudioManager.instance.volumeSetting.totalVolume = value);
        slider[1].onValueChanged.AddListener((value) => AudioManager.instance.volumeSetting.soundVolume = value);

        slider[2].onValueChanged.AddListener((value) => AudioManager.instance.volumeSetting.musicVolume = value);
    }


}
