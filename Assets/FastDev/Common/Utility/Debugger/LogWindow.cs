using System.Collections.Generic;
using UnityEngine;
namespace FastDev
{
    class LogWindow : IWindow
    {
        private int maxCount = 999;
        private List<Log> logs = new List<Log>();

        private Vector2 scrollPos;

        private string selectedLog;
        public LogWindow()
        {
            Application.logMessageReceivedThreaded += Application_logMessageReceivedThreaded;
        }

        private void Application_logMessageReceivedThreaded(string condition, string stackTrace, LogType type)
        {
            if (type == LogType.Error || type == LogType.Exception)
            {
                Log log = new Log(condition, stackTrace, type);
                if (logs.Count >= maxCount)
                    logs.RemoveAt(0);
                logs.Add(log);
            }
        }

        public void Draw()
        {
            scrollPos = GUILayout.BeginScrollView(scrollPos, new GUIStyle("box"));
            foreach (var item in logs)
            {
                if (GUILayout.Button(item.ToString(), new GUIStyle("label")))
                {
                    selectedLog = item.stackTrace;
                }
            }
            GUILayout.EndScrollView();
            if (!string.IsNullOrEmpty(selectedLog))
            {
                if (GUILayout.Button(selectedLog, new GUIStyle("label")))
                {
                    selectedLog = null;
                }
            }
        }
    }
}
