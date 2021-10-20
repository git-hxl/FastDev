using System;
using LitJson;
using System.Runtime.InteropServices;
namespace Bigger
{
    public static class SerializeUtil
    {
        /// <summary>
        /// 将对象转为字节数组
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ToByte(this object obj)
        {
            int size = Marshal.SizeOf(obj);
            byte[] data = new byte[size];
            IntPtr bufferIntPtr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(obj, bufferIntPtr, true);
                Marshal.Copy(bufferIntPtr, data, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(bufferIntPtr);
            }
            return data;
        }
        /// <summary>
        /// 将字节数组转为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T ToObject<T>(this byte[] data)
        {
            object obj;
            int size = Marshal.SizeOf(typeof(T));
            IntPtr allocIntPtr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(data, 0, allocIntPtr, size);
                obj = Marshal.PtrToStructure(allocIntPtr, typeof(T));
            }
            finally
            {
                Marshal.FreeHGlobal(allocIntPtr);
            }
            return (T)obj;
        }

        public static string ToJson(this object o, bool prettyPrint = false)
        {
            JsonWriter jsonWriter = new JsonWriter();
            jsonWriter.PrettyPrint = prettyPrint;
            JsonMapper.ToJson(o, jsonWriter);
            return System.Text.RegularExpressions.Regex.Unescape(jsonWriter.TextWriter.ToString());
        }

        public static T ToObject<T>(this string json)
        {
            return JsonMapper.ToObject<T>(json);
        }
    }
}