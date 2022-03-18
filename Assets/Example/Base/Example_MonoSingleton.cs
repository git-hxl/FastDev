using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastDev;
using UnityEngine.SceneManagement;

public class Example_MonoSingleton : MonoSingleton<Example_MonoSingleton>
{
    [ContextMenu("Load Scene")]
    public void LoadScene()
    {
        SceneManager.LoadScene("Base");
    }
}