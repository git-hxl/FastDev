using System;
namespace FastDev
{
    public static partial class Utility
    {
        public static partial class DateTime
        {
            /// <summary>
            /// 时间戳（毫秒）
            /// </summary>
            /// <returns></returns>
            public static long TimeStamp
            {
                get
                {
                    System.DateTime startTime = new System.DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    return (long)(System.DateTime.UtcNow - startTime).TotalMilliseconds;
                }
            }

            /// <summary>
            /// 转化时间戳
            /// </summary>
            /// <param name="timeStamp">时间戳（毫秒）</param>
            /// <returns>UTC DateTime</returns>
            public static System.DateTime ConvertToDateTime(long timeStamp)
            {
                System.DateTime startTime = new System.DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return startTime.AddMilliseconds(timeStamp);
            }
        }
    }

}