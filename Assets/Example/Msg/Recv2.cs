using System.Collections;
using System.Collections.Generic;
using System.Text;
using Bigger;
using UnityEngine;

public class Recv2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.Instance.Register(222, Handler);
    }

    void Handler(Hashtable data)
    {
        Debug.Log(gameObject.name + " recv:" + data[0]);
    }
}
