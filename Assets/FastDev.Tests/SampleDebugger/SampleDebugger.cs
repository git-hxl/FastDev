using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace FastDev
{
    public class SampleDebugger : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Console.Instance.RegisterCommand(ConsoleCommand.SystemInfo, Test);
            Console.Instance.RegisterCommand(ConsoleCommand.EnableLog, (args) => { Debugger.Instance.EnableLog(int.Parse(args[0].ToString())); return "ok"; });
            Console.Instance.RegisterCommand(ConsoleCommand.DisableLog, (args) => { Debugger.Instance.DisableLog(); return "ok"; });

            Console.Instance.RegisterCommand("Test", (args) => throw new System.Exception());
        }

        public string Test(params object[] args)
        {
            foreach (var item in args)
            {
                Debug.Log(item.ToString());
            }
            return FastDev.SystemInfo.ToString();
        }

        private void Update()
        {
            Debug.LogError(Time.frameCount + ":" + Utility.DateTime.TimeStamp);
            Debug.LogWarning(Time.frameCount + ":" + Utility.DateTime.TimeStamp);
            Debug.Log(Time.frameCount + ":" + Utility.DateTime.TimeStamp);
        }
    }
}
