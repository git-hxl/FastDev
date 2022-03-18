using System.Collections;
using System.Collections.Generic;
using System.Text;
using FastDev;
using UnityEngine;

public class Recv2 : MonoBehaviour
{
    Recv3 recv3;
    // Start is called before the first frame update
    void Start()
    {
        MsgManager.instance.Register(222, Handler);
        recv3 = new Recv3();
    }

    void Handler(Hashtable data)
    {
        Debug.Log(gameObject.name + " recv:" + data[0]);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        recv3 = null;
    }

    [ContextMenu("GC")]
    private void GC()
    {
        System.GC.Collect();
    }

}

public class Recv3
{
    public Recv3()
    {
        MsgManager.instance.Register(222, Handler);
    }

    ~Recv3()
    {
        Debug.LogError("recv3 destroy!!!");
    }

    public void Handler(Hashtable data)
    {
        Debug.Log(this.GetHashCode() + " recv:" + data[0]);
    }
}
