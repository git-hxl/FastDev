using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEntry.Sound.GetSoundAgent(SoundType.Music).Play("Assets/Arts/Sound/SFX_FireThrower_Fire_Loop.wav");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            GameEntry.Sound.GetSoundAgent(SoundType.UI).Play("Assets/Arts/Sound/UIClick.wav");
        }
    }
}
