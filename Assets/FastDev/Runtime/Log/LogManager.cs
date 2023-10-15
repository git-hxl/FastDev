using System.IO;
using System.Text;
using UnityEngine;

namespace FastDev
{
    public class LogManager : MonoSingleton<LogManager>
    {
        public string LogPath { get; private set; }

        /// <summary>
        /// Log：会显示所有类型的Log。
        /// Warning：会显示Warning,Assert,Error,Exception
        /// Assert：会显示Assert，Error，Exception
        /// Error：显示Error和Exception
        /// Exception：只会显示Exception
        /// </summary>
        public LogType FilterLogType = LogType.Error;

        protected override void OnInit()
        {
            base.OnInit();
            LogPath = "./log.txt";
            Application.logMessageReceivedThreaded += Application_logMessageReceivedThreaded;
            EnableLog();
        }


        private void OnDestroy()
        {
            Application.logMessageReceivedThreaded -= Application_logMessageReceivedThreaded;

            DisableLog();
        }

        private void Application_logMessageReceivedThreaded(string condition, string stackTrace, LogType type)
        {
            using (FileStream fileStream = new FileStream(LogPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                LogData log = new LogData(condition, stackTrace, type);
                byte[] data = Encoding.UTF8.GetBytes(log.ToString());
                fileStream.Write(data, 0, data.Length);
            }
        }

        public void EnableLog()
        {
            Debug.unityLogger.logEnabled = true;
            Debug.unityLogger.filterLogType = FilterLogType;
        }

        public void EnableLog(int logType)
        {
            FilterLogType = (LogType)logType;
            EnableLog();
        }

        public void DisableLog()
        {
            Debug.unityLogger.logEnabled = false;
        }
    }
}