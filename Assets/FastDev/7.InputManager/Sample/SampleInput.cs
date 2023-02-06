using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public class SampleInput : MonoBehaviour
    {
        private void Start()
        {
            InputManager.Instance.RegisterAction("W", new InputData() { KeyCodes = new List<KeyCode>() { KeyCode.W } });
            InputManager.Instance.RegisterAction("A", new InputData() { KeyCodes = new List<KeyCode>() { KeyCode.A } });
            InputManager.Instance.RegisterAction("S", new InputData() { KeyCodes = new List<KeyCode>() { KeyCode.S } });
            InputManager.Instance.RegisterAction("D", new InputData() { KeyCodes = new List<KeyCode>() { KeyCode.D } });

            InputManager.Instance.RegisterAction("¼ô¼­", new InputData() { KeyCodes = new List<KeyCode>() { KeyCode.LeftControl, KeyCode.X } });
            InputManager.Instance.RegisterAction("¸´ÖÆ", new InputData() { KeyCodes = new List<KeyCode>() { KeyCode.LeftControl, KeyCode.C } });
            InputManager.Instance.RegisterAction("Õ³Ìù", new InputData() { KeyCodes = new List<KeyCode>() { KeyCode.LeftControl, KeyCode.V } });

            InputManager.Instance.RegisterAction("¿Õ¸ñ", new InputData() { KeyCodes = new List<KeyCode>() { KeyCode.Space } });

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


            if (InputManager.Instance.GetKeyDown("¸´ÖÆ"))
            {
                Debug.Log("¸´ÖÆ");
            }

            if (InputManager.Instance.GetKeyDown("Õ³Ìù"))
            {
                Debug.Log("Õ³Ìù");
            }

            if (InputManager.Instance.GetKeyUp("¼ô¼­"))
            {
                Debug.Log("¼ô¼­");
            }

            if (InputManager.Instance.GetKeyUp("¿Õ¸ñ"))
            {
                Debug.Log("¿Õ¸ñ");
            }


            if (InputManager.Instance.GetAxis("Mouse X") > 0f)
            {
                Debug.Log("Mouse X");
            }


            if (InputManager.Instance.GetKeyUp("A") || InputManager.Instance.GetKeyUp("D"))
            {
                Debug.Log("AD:" + Time.frameCount);
            }

            if ( InputManager.Instance.GetKeyDown("S"))
            {
                Debug.Log("WS:" + Time.frameCount);
            }

            if (InputManager.Instance.GetKeyDown("W") )
            {
                Debug.Log("WS:" + Time.frameCount);
            }
        }
    }
}
