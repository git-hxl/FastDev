using System.Net;
using System.Text.RegularExpressions;

namespace Bigger
{
    public static class StringExtension
    {
        /// <summary>
        /// 从路径中获取文件名
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isFullName">是否包含后缀名</param>
        /// <returns></returns>
        public static string GetFileName(this string path, bool isFullName = true)
        {
            int startIndex = path.LastIndexOf('/') + 1;
            if (isFullName)
                return path.Substring(startIndex);

            int endIndex = path.LastIndexOf('.');
            if (endIndex == -1)
                return path.Substring(startIndex);
            return path.Substring(startIndex, endIndex - startIndex);

        }
        public static IPAddress ParseIP(this string address)
        {
            IPAddress ip;
            if (!IPAddress.TryParse(address, out ip))
            {
                IPHostEntry hostInfo = Dns.GetHostEntry(address);
                ip = hostInfo.AddressList[0];
            }
            return ip;
        }

        /// <summary>
        /// 只保留字母数字
        /// </summary>
        /// <param name="s">是否移除空格</param>
        /// <returns></returns>
        public static string ToAlphaNumber(this string s, bool isTrim = true)
        {
            string pattern = @"[^a-zA-Z0-9\s]";
            if (isTrim)
                pattern = @"[^a-zA-Z0-9]";
            s = Regex.Replace(s, pattern, "");
            return s;
        }
        /// <summary>
        /// 只保留字母数字汉字
        /// </summary>
        /// <param name="s">是否移除空格</param>
        /// <returns></returns>
        public static string ToAlphaNumberAndChinese(this string s, bool isTrim = true)
        {
            string pattern = @"[^a-zA-Z0-9\u4e00-\u9fa5\s]";
            if (isTrim)
                pattern = @"[^a-zA-Z0-9\u4e00-\u9fa5]";
            s = Regex.Replace(s, pattern, "");
            return s;
        }
    }

}