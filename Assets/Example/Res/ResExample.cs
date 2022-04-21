using FastDev.Res;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class ResExample : MonoBehaviour
{
    public Texture2D texture2D;
    public AudioClip audioClip;
    // Start is called before the first frame update
    async UniTaskVoid Start()
    {
        await ResManager.instance.LoadAllAssetBundle();
        texture2D = ResManager.instance.LoadAsset<Texture2D>(ABConstant.texture2d, "Assets/Example/Res/纹理.jpg");
        audioClip = ResManager.instance.LoadAsset<AudioClip>(ABConstant.audio, "Assets/Example/Res/shot.mp3");

        Instantiate(ResManager.instance.LoadAsset<GameObject>(ABConstant.prefab, "Assets/Example/Res/Cube.prefab"));

        string[] deps = ResManager.instance.GetAllDependencies(ABConstant.prefab);
        foreach (var item in deps)
        {
            Debug.Log("prefab dependencies:" + item);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
