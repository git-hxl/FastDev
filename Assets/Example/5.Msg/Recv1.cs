using System.Collections;
using System.Collections.Generic;
using System.Text;
using FastDev;
using UnityEngine;

public class Recv1 : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        MsgManager.Instance.Register(111, Handler);
    }

    void Handler(Hashtable data)
    {
        Debug.Log(gameObject.name + " recv:" + data[0]);
    }

    private void OnDestroy()
    {
        MsgManager.Instance.UnRegister(111, Handler);
    }
}
