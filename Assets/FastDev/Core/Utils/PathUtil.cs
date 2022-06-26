using System.IO;

namespace FastDev
{
    public static class PathUtil
    {
        public static string GetFileName(this string path)
        {
            return Path.GetFileName(path);
        }

        public static string GetFileNameWithoutExtension(this string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
    }
}
