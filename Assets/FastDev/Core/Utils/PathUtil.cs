namespace FastDev
{
    public static class PathUtil
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
    }
}
