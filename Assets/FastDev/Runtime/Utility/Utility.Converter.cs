namespace FastDev
{
    public static partial class Utility
    {
        public static partial class Converter
        {
            /// <summary>
            ///字节单位转换
            /// </summary>
            /// <param name="value"></param>
            /// <returns></returns>
            public static string ByteConvert(long value)
            {
                if (value < 1024L)
                {
                    return value.ToString() + " B";
                }
                if (value < 1024L * 1024L)
                {
                    return (value / 1024f).ToString("F2") + " KB";
                }
                if (value < 1024L * 1024L * 1024L)
                {
                    return (value / 1024f / 1024f).ToString("F2") + " MB";
                }
                if (value < 1024L * 1024L * 1024L * 1024L)
                {
                    return (value / 1024f / 1024f / 1024f).ToString("F2") + " GB";
                }
                return (value / 1024f / 1024f / 1024f / 1024f).ToString("F2") + " TB";
            }

            /// <summary>
            /// 数字单位转换
            /// </summary>
            /// <param name="value">数值</param>
            /// <returns></returns>
            public static string ValueConvert(long value)
            {
                if (value <= 1000)
                {
                    return value.ToString();
                }
                else if (value < 1000000)
                {
                    return (value / 1000f).ToString("N2") + "K";//千
                }
                else if (value < 1000000000)
                {
                    return (value / 1000f / 1000f).ToString("N2") + "M";//百万
                }
                else if (value < 1000000000000)
                {
                    return (value / 1000f / 1000f / 1000f).ToString("N2") + "B";//十亿
                }
                else
                {
                    return (value / 1000f / 1000f / 1000f / 1000f).ToString("N2") + "T";//万亿
                }
            }
        }
    }
}