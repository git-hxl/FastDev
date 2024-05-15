
using System.IO;
using System.Text;
using UnityEngine;

namespace FastDev
{
    public static class Debugger
    {
        private static string logPath = "./log.txt";
        private static FileStream writeLogStream;

        /// <summary>
        /// 开关日志
        /// </summary>
        /// <param name="toggle"></param>
        public static void SetLogToggle(bool toggle)
        {
            UnityEngine.Debug.unityLogger.logEnabled = toggle;
        }

        /// <summary>
        /// 设置日志等级
        /// </summary>
        /// <param name="logLevel"></param>
        public static void SetLogLevel(LogType logLevel)
        {
            UnityEngine.Debug.unityLogger.filterLogType = logLevel;
        }

        /// <summary>
        /// 打开日志记录
        /// </summary>
        public static void OpenLogRecord()
        {
            writeLogStream = new FileStream(logPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

            Application.logMessageReceivedThreaded += OnLogReceived;
        }

        /// <summary>
        /// 关闭日志记录
        /// </summary>
        public static void CloseLogRecord()
        {
            if (writeLogStream != null)
            {
                writeLogStream.Close();
                writeLogStream = null;
            }

            Application.logMessageReceivedThreaded -= OnLogReceived;
        }

        private static void OnLogReceived(string condition, string stackTrace, LogType type)
        {
            if (writeLogStream == null)
                return;

            if (type == LogType.Error || type == LogType.Exception)
            {
                LogData log = ReferencePool.Acquire<LogData>();
                log.Init(condition, stackTrace, type);
                byte[] data = Encoding.UTF8.GetBytes(log.ToString());
                writeLogStream.Write(data, 0, data.Length);

                ReferencePool.Release(log);
            }
        }
    }
}
