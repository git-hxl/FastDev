using System;
using System.Text;
using UnityEngine;

namespace FastDev
{
    class Log
    {
        public string condition;
        public string stackTrace;
        public LogType logType;
        public string date;

        private string str;
        public Log(string condition, string stackTrace, LogType logType)
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
                //if (logType == LogType.Assert || logType == LogType.Error || logType == LogType.Exception)
                stringBuilder.Append("</color>");
                //stringBuilder.Append(Environment.NewLine);
                //stringBuilder.Append(stackTrace);
                //stringBuilder.Append(Environment.NewLine);
                str = stringBuilder.ToString();
            }
            return str;
        }
    }
}
