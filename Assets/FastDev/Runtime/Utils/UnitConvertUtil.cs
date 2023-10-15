namespace FastDev
{
    public static class UnitConvertUtil
    {
        /// <summary>
        /// 存储单位换算
        /// </summary>
        /// <param name="size">字节</param>
        /// <returns></returns>
        public static string ByteConvert(long size)
        {
            return ByteConvert((float)size);
        }
        public static string ByteConvert(float size)
        {
            if (size < 1024L)
            {
                return size.ToString() + " B";
            }
            if (size < 1024L * 1024L)
            {
                return (size / 1024f).ToString("F2") + " KB";
            }
            if (size < 1024L * 1024L * 1024L)
            {
                return (size / 1024f / 1024f).ToString("F2") + " MB";
            }
            if (size < 1024L * 1024L * 1024L * 1024L)
            {
                return (size / 1024f / 1024f / 1024f).ToString("F2") + " GB";
            }
            return (size / 1024f / 1024f / 1024f / 1024f).ToString("F2") + " TB";
        }

        /// <summary>
        /// 数字单位转换
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns></returns>
        public static string LongConvert(long value)
        {
            if (value <= 1000)
            {
                return value.ToString();
            }
            else if (value < 1000000)
            {
                return (value / 1000f).ToString("N1") + "K";
            }
            else if (value < 1000000000)
            {
                return (value / 1000f / 1000f).ToString("N2") + "M";
            }
            else if (value < 1000000000000)
            {
                return (value / 1000f / 1000f / 1000f).ToString("N3") + "B";
            }
            else
            {
                return (value / 1000f / 1000f / 1000f / 1000f).ToString("N4") + "T";
            }
        }
    }
}