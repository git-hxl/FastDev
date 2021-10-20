using System.Collections;
using System.Collections.Generic;
using Bigger;
using UnityEngine;

public class ResExample : MonoBehaviour
{
    public Sprite sprite;
    public Texture texture;
    public Texture2D texture2D;
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
    private void UpdateProcess(string name,float process)
    {
        Debug.Log(name + ":" + process);
    }
    void UpdateError()
    {
        Debug.LogError("更新失败");
    }

    void InitSuccess()
    {
        Debug.Log("资源初始化完成");
        //Instantiate(ResManager.Instance.LoadAsset<GameObject>("prefab", "Assets/Example/Pool/Cube.prefab"));
        sprite = ResManager.Instance.LoadAsset<Sprite>("texture2d", "Assets/Example/Res/1.jpg");
        texture = ResManager.Instance.LoadAsset<Texture>("texture2d", "Assets/Example/Res/1.jpg");
        texture2D = ResManager.Instance.LoadAsset<Texture2D>("texture2d", "Assets/Example/Res/1.jpg");

        string[] d = ResManager.Instance.GetAllDependencies("prefab");
        foreach (var item in d)
        {
            Debug.Log("AB包 依赖：" + item);
        }
    }
}
