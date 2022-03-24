using LitJson;
using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Google.Protobuf;
using System.Text;

namespace FastDev
{
    public static class Serializer
    {

        public static string ByteToString(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static byte[] StringToByte(this string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }

        /// <summary>
        /// 对象转Json
        /// </summary>
        /// <param name="o"></param>
        /// <param name="prettyPrint"></param>
        /// <returns></returns>
        public static string ObjectToJson(this object o, bool prettyPrint = false)
        {
            JsonWriter jsonWriter = new JsonWriter();
            jsonWriter.PrettyPrint = prettyPrint;
            JsonMapper.ToJson(o, jsonWriter);
            return Regex.Unescape(jsonWriter.ToString());
        }
        /// <summary>
        /// Json转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(this string json)
        {
            return JsonMapper.ToObject<T>(json);
        }

        /// <summary>
        /// Proto转字节
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static byte[] ProtoToByte(this IMessage o)
        {
            return o.ToByteArray();
        }
        /// <summary>
        /// 字节转Proto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T ByteToProto<T>(this byte[] bytes) where T : IMessage<T>,new()
        {
            var parser = new MessageParser<T>(() => new T());
            return parser.ParseFrom(bytes);
        }


        /// <summary>
        /// 将对象转为字节数组
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ObjectToByte(this object obj)
        {
            int size = Marshal.SizeOf(obj);
            byte[] bytes = new byte[size];
            IntPtr bufferIntPtr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(obj, bufferIntPtr, false);
                Marshal.Copy(bufferIntPtr, bytes, 0, size);
            }
            finally
            {
                Marshal.FreeHGlobal(bufferIntPtr);
            }
            return bytes;
        }
        /// <summary>
        /// 将字节数组转为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T ByteToObject<T>(this byte[] bytes)
        {
            object obj;
            int size = Marshal.SizeOf(typeof(T));
            IntPtr allocIntPtr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(bytes, 0, allocIntPtr, size);
                obj = Marshal.PtrToStructure(allocIntPtr, typeof(T));
            }
            finally
            {
                Marshal.FreeHGlobal(allocIntPtr);
            }
            return (T)obj;
        }
    }

}