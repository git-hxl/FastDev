using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bigger;
using UnityEngine.SceneManagement;

public class MonoSingleton2 : MonoSingleton<MonoSingleton1>
{
    protected override void Init()
    {
        base.Init();
        Debug.Log(gameObject.name + " init");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    [ContextMenu("Load Scene")]
    public void LoadScene()
    {
        SceneManager.LoadScene("Scene1");
    }
}
