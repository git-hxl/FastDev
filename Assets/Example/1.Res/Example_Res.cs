using System.Collections;
using System.Collections.Generic;
using Bigger;
using UnityEngine;

public class Example_Res : MonoBehaviour
{
    public Texture2D texture2D;
    public AudioClip audioClip;
    private void Awake()
    {
        ResManager.Instance.updateErrorCallback += UpdateError;
        ResManager.Instance.initSuccessCallback += InitSuccess;
        ResManager.Instance.downloadCallback += UpdateProcess;
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void UpdateProcess(string name, float process)
    {
        Debug.Log(name + ":" + process);
    }
    void UpdateError()
    {
        Debug.LogError("更新失败");
    }

    async void InitSuccess()
    {
        Debug.Log("资源初始化完成");
        if (ResManager.Instance.resLoadType != ResLoadType.FromEditor)
            await ResManager.Instance.LoadAllAssetBundle();
        texture2D = ResManager.Instance.LoadAsset<Texture2D>("texture2d", "Assets/Example/1.Res/纹理.jpg");
        audioClip = ResManager.Instance.LoadAsset<AudioClip>("audio", "Assets/Example/1.Res/shot.mp3");
        AudioManager.Instance.PlayClip("Assets/Example/1.Res/shot.mp3");
    }
}
