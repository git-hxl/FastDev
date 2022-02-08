using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Cysharp.Threading.Tasks;
namespace FastDev
{
    public class Debugger : MonoSingleton<Debugger>
    {
        public bool outFile = true;
        public bool onlyError = true;
        private string logPath;
        private bool openWindow;
        private bool lockScroll = true;
        private Vector2 firstDown;
        private List<LogInfo> logInfos = new List<LogInfo>();
        private Rect consoleWindow = new Rect(0, 0, Screen.width, Screen.height);
        private int curFrame;
        private float curTime;
        private int fps;
        protected override void Init()
        {
            logPath = Application.persistentDataPath + "/log.txt";
            if (File.Exists(logPath))
                File.Delete(logPath);
            Application.logMessageReceivedThreaded += Application_logMessageReceived;
        }
        private void Update()
        {
            curFrame++;
            curTime += Time.deltaTime;
            if (curTime >= 1)
            {
                fps = (int)(curFrame / curTime);
                curFrame = 0;
                curTime = 0;
            }
        }

        private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
        {
            if (onlyError && (type == LogType.Log || type == LogType.Warning))
                return;
            LogInfo logInfo = new LogInfo(condition, stackTrace, type);
            if (outFile)
            {
                File.AppendAllText(logPath, logInfo.ToString());
            }
            if (logInfos.Count >= 999)
                logInfos.RemoveAt(0);
            logInfos.Add(logInfo);
        }

        private void OnGUI()
        {
            GUI.skin.button.fontSize = 30;
            GUI.skin.label.fontSize = 30;
            GUI.skin.label.alignment = TextAnchor.MiddleLeft;
            GUI.skin.box.fontSize = 30;
            GUI.skin.box.alignment = TextAnchor.MiddleLeft;
            GUI.skin.button.fixedWidth = 120;
            GUI.skin.button.fixedHeight = 80;
            if (!openWindow && GUILayout.Button(fps.ToString()))
                openWindow = true;

            if (openWindow)
                consoleWindow = GUI.Window(0, consoleWindow, DrawWindow, "Debugger");

            if (Event.current.type == EventType.MouseDown)
            {
                firstDown = Event.current.mousePosition;
            }
            if (Event.current.type == EventType.MouseDrag && !lockScroll)
            {
                Vector2 dir = (Event.current.mousePosition - firstDown);

                scrollPos -= dir;
                firstDown = Event.current.mousePosition;
            }
        }
        Vector2 scrollPos;
        private void DrawWindow(int id)
        {
            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button("Close"))
                openWindow = false;
            if (GUILayout.Button("Clear"))
                logInfos.Clear();
            GUILayout.Label(logPath, GUILayout.MinHeight(80));
            string btLockName = lockScroll ? "UnLock" : "Lock";
            if (GUILayout.Button(btLockName))
                lockScroll = !lockScroll;
            if (GUILayout.Button("Top"))
            {
                scrollPos = Vector2.zero;
                lockScroll = false;
            }
            GUILayout.EndHorizontal();
            if (lockScroll)
                scrollPos.y = int.MaxValue;

            scrollPos = GUILayout.BeginScrollView(scrollPos, false, false);

            foreach (var item in logInfos)
            {
                GUILayout.Box(item.ToString());
            }

            GUILayout.EndScrollView();
        }
    }

    public class LogInfo
    {
        public string condition;
        public string stackTrace;
        public LogType logType;
        public string date;

        private string str;
        public LogInfo(string condition, string stackTrace, LogType logType)
        {
            this.condition = condition;
            this.stackTrace = stackTrace;
            this.logType = logType;
            this.date = DateTime.Now.ToString();
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(str))
            {
                StringBuilder stringBuilder = new StringBuilder();
                if (logType == LogType.Assert || logType == LogType.Error || logType == LogType.Exception)
                    stringBuilder.Append("<color=#FF0000>");
                stringBuilder.Append("[");
                stringBuilder.Append(date);
                stringBuilder.Append("] ");
                stringBuilder.Append(condition);
                if (logType == LogType.Assert || logType == LogType.Error || logType == LogType.Exception)
                    stringBuilder.Append("</color>");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append(stackTrace);
                stringBuilder.Append(Environment.NewLine);
                str = stringBuilder.ToString();
            }
            return str;
        }
    }
}