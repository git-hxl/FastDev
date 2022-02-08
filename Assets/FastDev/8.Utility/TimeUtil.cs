using System;
namespace FastDev
{
    public static class TimeUtil
    {
        public static long GetCurTimestamp()
        {
            return DateTimeToTimestamp(DateTime.UtcNow);
        }
        /// <summary>
        /// DateTime转时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long DateTimeToTimestamp(DateTime dateTime)
        {
            TimeSpan ts = dateTime - new DateTime(1970, 1, 1);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
        /// <summary>
        /// 时间戳转DateTime(当地时区)
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime TimestampToDateTime(long timestamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1).ToLocalTime();
            return dateTime.AddMilliseconds(timestamp);
        }
    }
}