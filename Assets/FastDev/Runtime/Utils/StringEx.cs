using System;
using System.Net;
using System.Text.RegularExpressions;

namespace FastDev
{
    public static class StringEx
    {

        public static IPAddress ParseIP(this string str)
        {
            IPAddress ip;
            if (!IPAddress.TryParse(str, out ip))
            {
                IPHostEntry hostInfo = Dns.GetHostEntry(str);
                ip = hostInfo.AddressList[0];
            }
            return ip;
        }
        /// <summary>
        /// 只保留字母数字
        /// </summary>
        /// <param name="s">是否移除空格</param>
        /// <returns></returns>
        public static string ToAlphaNumber(this string str, bool isTrim = true)
        {
            string pattern = @"[^a-zA-Z0-9\s]";
            if (isTrim)
                pattern = @"[^a-zA-Z0-9]";
            str = Regex.Replace(str, pattern, "");
            return str;
        }
        /// <summary>
        /// 只保留字母数字汉字
        /// </summary>
        /// <param name="s">是否移除空格</param>
        /// <returns></returns>
        public static string ToAlphaNumberAndChinese(this string str, bool isTrim = true)
        {
            string pattern = @"[^a-zA-Z0-9\u4e00-\u9fa5\s]";
            if (isTrim)
                pattern = @"[^a-zA-Z0-9\u4e00-\u9fa5]";
            str = Regex.Replace(str, pattern, "");
            return str;
        }

        /// <summary>
        /// Unicode转中文
        /// </summary>
        /// <returns></returns>
        public static string UnicodeToChinese(this string str)
        {
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
                  str, x => string.Empty + Convert.ToChar(Convert.ToInt32(x.Result("$1"), 16)));
        }

        public static bool IsChinese(this string str)
        {
            return Regex.IsMatch(str, @"[\u4e00-\u9fa5]");
        }
    }

}