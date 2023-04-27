using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Framework
{
    public class SampleDebugger : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Console.Instance.RegisterCommand(ConsoleCommand.SystemInfo, Test);
            Console.Instance.RegisterCommand(ConsoleCommand.EnableLog, (args) => { LogManager.Instance.EnableLog(int.Parse(args[0].ToString())); return "ok"; });
            Console.Instance.RegisterCommand(ConsoleCommand.DisableLog, (args) => { LogManager.Instance.DisableLog(); return "ok"; });

            Console.Instance.RegisterCommand("Test", (args) => throw new System.Exception());
        }

        public string Test(params object[] args)
        {
            foreach (var item in args)
            {
                Debug.Log(item.ToString());
            }
            return Framework.SystemInfo.ToString();
        }

        private void Update()
        {
            Debug.LogError(Time.frameCount + ":" + DateTimeUtil.TimeStamp);
            Debug.LogWarning(Time.frameCount + ":" + DateTimeUtil.TimeStamp);
            Debug.Log(Time.frameCount + ":" + DateTimeUtil.TimeStamp);
        }
    }
}
