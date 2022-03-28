using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class Example_UniTask : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        Debug.Log(Time.realtimeSinceStartup);
        await UniTask.SwitchToThreadPool();
        Thread.Sleep(6000);
        await UniTask.SwitchToMainThread();
        Debug.Log(Time.realtimeSinceStartup);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
