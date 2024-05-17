using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            FastDev.GameEntry.Message.Dispatch(-1, 111111);
            //FastDev.GameEntry.Message.Dispatch(-2, 111111);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            //FastDev.GameEntry.Message.Dispatch(-1, 111111, "22222222");
            FastDev.GameEntry.Message.Dispatch(-2, 111111, "22222222");
        }
    }
}
