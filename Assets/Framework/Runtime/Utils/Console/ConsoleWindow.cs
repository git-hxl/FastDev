using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameFramework
{
    public class ConsoleWindow : MonoSingleton<ConsoleWindow>, IConsoleWindow
    {
        private float windowWidth;
        private float windowHeight;

        private string inputStr;

        private Vector2 scorllPos;

        private bool activeWindow;

        private GUIStyle textStyle = new GUIStyle();

        private List<string> inputStrs = new List<string>();
        private List<string> outputStrs = new List<string>();
        private void Start()
        {
            windowWidth = Screen.width / 3f;
            windowHeight = Screen.height / 3f;
            textStyle.normal.textColor = Color.yellow;

            Console.Instance.RegisterCommand("systeminfo", (obj) => SystemInfo.ToString());
            Console.Instance.RegisterCommand("memoryinfo", (obj) => MemoryInfo.ToString());
            Console.Instance.RegisterCommand("clear", (obj) => { ClearOutput(); return ""; });
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.BackQuote))
            {
                activeWindow = !activeWindow;
            }
        }

        private void OnGUI()
        {
            if (!activeWindow)
                return;
            GUI.color = Color.green;
            GUILayout.Window(0, new Rect(0, Screen.height - windowHeight, windowWidth, windowHeight), DrawWindow, "Console");
            GUI.FocusWindow(0);
        }

        public void DrawWindow(int id)
        {
            DrawOutput();

            DrawInput();
        }

        public void DrawInput()
        {
            GUI.SetNextControlName("input");
            inputStr = GUILayout.TextField(inputStr);

            if (Event.current.keyCode == KeyCode.Return && Event.current.type == EventType.KeyUp)
            {
                if (GUI.GetNameOfFocusedControl() != "input")
                {
                    GUI.FocusControl("input");
                    return;
                }

                if (!string.IsNullOrEmpty(inputStr))
                {
                    inputStrs.Add(inputStr);
                    inputStr = inputStr.TrimEnd();
                    outputStrs.Add(">" + inputStr);
                    string[] commands = inputStr.Split(' ');
                    int indexArg = inputStr.IndexOf(' ');
                    if (indexArg != -1)
                    {
                        object[] args = inputStr.Substring(indexArg + 1).Split(' ');
                        outputStrs.Add(Console.Instance.Execute(commands[0], args));
                    }
                    else
                        outputStrs.Add(Console.Instance.Execute(commands[0]));
                    inputStr = "";
                }
            }

            if (Event.current.keyCode == KeyCode.UpArrow && Event.current.type == EventType.KeyUp)
            {
                BackToLastInput();
            }

        }

        public void DrawOutput()
        {
            scorllPos = GUILayout.BeginScrollView(scorllPos, false, false);
            foreach (var item in outputStrs)
            {
                GUILayout.Label(item, textStyle);
            }
            GUILayout.EndScrollView();
        }

        public void BackToLastInput()
        {
            if (inputStrs.Count > 0)
            {
                inputStr = inputStrs.Last();
                inputStrs.RemoveAt(inputStrs.Count - 1);
            }
        }

        public void ClearOutput()
        {
            outputStrs.Clear();
        }
    }
}