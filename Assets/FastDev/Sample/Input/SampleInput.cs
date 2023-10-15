using FastDev;
using System.Collections.Generic;
using UnityEngine;

public class SampleInput : MonoBehaviour
{
    private void Start()
    {
        InputManager.Instance.RegisterAction("W", new InputData() { KeyCodes = new List<KeyCode>() { KeyCode.W } });
        InputManager.Instance.RegisterAction("A", new InputData() { KeyCodes = new List<KeyCode>() { KeyCode.A } });
        InputManager.Instance.RegisterAction("S", new InputData() { KeyCodes = new List<KeyCode>() { KeyCode.S } });
        InputManager.Instance.RegisterAction("D", new InputData() { KeyCodes = new List<KeyCode>() { KeyCode.D } });

        InputManager.Instance.RegisterAction("剪辑", new InputData() { KeyCodes = new List<KeyCode>() { KeyCode.LeftControl, KeyCode.X } });
        InputManager.Instance.RegisterAction("复制", new InputData() { KeyCodes = new List<KeyCode>() { KeyCode.LeftControl, KeyCode.C } });
        InputManager.Instance.RegisterAction("粘贴", new InputData() { KeyCodes = new List<KeyCode>() { KeyCode.LeftControl, KeyCode.V } });

        InputManager.Instance.RegisterAction("空格", new InputData() { KeyCodes = new List<KeyCode>() { KeyCode.Space } });

        InputManager.Instance.RegisterAction("Mouse X", new InputData() { Type = InputData.InputType.Axes, Axis = "Mouse X" });

        InputManager.Instance.Save();
    }


    private void Update()
    {
        //if (InputManager.Instance.GetKey("W"))
        //{
        //    Debug.Log("W");
        //}
        //if (InputManager.Instance.GetKey("A"))
        //{
        //    Debug.Log("A");
        //}
        //if (InputManager.Instance.GetKey("S"))
        //{
        //    Debug.Log("S");
        //}
        //if (InputManager.Instance.GetKey("D"))
        //{
        //    Debug.Log("D");
        //}


        if (InputManager.Instance.GetKeyDown("复制"))
        {
            Debug.Log("复制");
        }

        if (InputManager.Instance.GetKeyDown("粘贴"))
        {
            Debug.Log("粘贴");
        }

        if (InputManager.Instance.GetKeyUp("剪辑"))
        {
            Debug.Log("剪辑");
        }

        if (InputManager.Instance.GetKeyUp("空格"))
        {
            Debug.Log("空格");
        }


        //if (InputManager.Instance.GetAxis("Mouse X") > 0f)
        //{
        //    Debug.Log("Mouse X");
        //}


        if (InputManager.Instance.GetKeyUp("A"))
        {
            Debug.Log("A:" + Time.frameCount);
        }

        if (InputManager.Instance.GetKeyUp("D") )
        {
            Debug.Log("D:" + Time.frameCount);
        }

        if (InputManager.Instance.GetKeyDown("S"))
        {
            Debug.Log("S:" + Time.frameCount);
        }

        if (InputManager.Instance.GetKeyDown("W"))
        {
            Debug.Log("W:" + Time.frameCount);
        }
    }
}