using System.IO;
using System.Text;
using UnityEngine;

namespace FastDev
{
    public class DebuggerLog : MonoSingleton<DebuggerLog>
    {
        private FileStream fileStream;
        public string LogPath { get; private set; }

        /// <summary>
        /// Log：会显示所有类型的Log。
        /// Warning：会显示Warning,Assert,Error,Exception
        /// Assert：会显示Assert，Error，Exception
        /// Error：显示Error和Exception
        /// Exception：只会显示Exception
        /// </summary>
        public LogType FilterLogType = LogType.Error;

        protected override void Awake()
        {
            base.Awake();
            LogPath = "./log.txt";
        }

        private void Start()
        {
            EnableLog();
        }

        private void OnDestroy()
        {
            DisableLog();
        }

        private void Application_logMessageReceivedThreaded(string condition, string stackTrace, LogType type)
        {
            LogData log = new LogData(condition, stackTrace, type);
            byte[] data = Encoding.UTF8.GetBytes(log.ToString());
            fileStream.Write(data, 0, data.Length);
        }

        public void EnableLog()
        {
            if (fileStream == null)
            {
                fileStream = new FileStream(LogPath, FileMode.Create, FileAccess.ReadWrite);
                Application.logMessageReceivedThreaded += Application_logMessageReceivedThreaded;
            }
            Debug.unityLogger.logEnabled = true;
            Debug.unityLogger.filterLogType = FilterLogType;
        }

        public void EnableLog(int level)
        {
            FilterLogType = (LogType)level;
            EnableLog();
        }

        public void DisableLog()
        {
            Debug.unityLogger.logEnabled = false;
            if (fileStream != null)
            {
                fileStream.Close();
                fileStream = null;
                Application.logMessageReceivedThreaded -= Application_logMessageReceivedThreaded;
            }
        }
    }
}