using System;
using System.Text;
using UnityEngine;

namespace FastDev
{
    class LogData : IReference
    {
        public string condition;
        public string stackTrace;
        public LogType logType;
        public string date;

        public void Init(string condition, string stackTrace, LogType logType)
        {
            this.condition = condition;
            this.stackTrace = stackTrace;
            this.logType = logType;
            this.date = DateTime.Now.ToString();
        }

        public void OnClear()
        {
            condition = null;
            stackTrace = null;
            date = null;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(date);
            stringBuilder.Append("：");
            stringBuilder.AppendLine(condition);
            stringBuilder.AppendLine(stackTrace);
            return stringBuilder.ToString();
        }
    }
}
