using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class UniTaskExample : MonoBehaviour
{
    // Start is called before the first frame update
    async UniTaskVoid Start()
    {
        await DelayFunc();
        Debug.Log(Time.time);
    }

    async UniTask DelayFunc()
    {
        await UniTask.Delay(1000);
        Debug.Log(Time.time);
    }
}
