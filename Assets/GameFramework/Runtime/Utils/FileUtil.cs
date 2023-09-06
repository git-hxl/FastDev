using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace GameFramework
{
    public static class FileUtil
    {
        /// <summary>
        /// 针对处理安卓平台的文件读取
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static async UniTask<byte[]> ReadFromStreamingAssets(string path)
        {
            if(path.Contains("://"))
            {
                UnityWebRequest web = UnityWebRequest.Get(path);
                var result = await web.SendWebRequest();
                return result.downloadHandler.data;
            }
            return File.ReadAllBytes(path);
        }


        public static Dictionary<string, string> LoadABHash(AssetBundleManifest manifest)
        {
            Dictionary<string, string> bundlesHash = new Dictionary<string, string>();
            string[] bundlesName = manifest.GetAllAssetBundles();
            for (int i = 0; i < bundlesName.Length; i++)
            {
                Hash128 hash = manifest.GetAssetBundleHash(bundlesName[i]);
                bundlesHash.Add(bundlesName[i], hash.ToString());
            }
            return bundlesHash;
        }


        public static string GetFileMD5(string path)
        {
            var hash = MD5.Create();
            var stream = new FileStream(path, FileMode.Open);
            byte[] hashByte = hash.ComputeHash(stream);
            stream.Close();
            hash.Dispose();
            return BitConverter.ToString(hashByte).ToLower().Replace("-", "");
        }

        public static string GetMD5(string content)
        {
            var data = Encoding.UTF8.GetBytes(content);
            return GetMD5(data);
        }

        public static string GetMD5(byte[] data)
        {
            var hash = MD5.Create();
            byte[] hashByte = hash.ComputeHash(data);
            hash.Dispose();
            return BitConverter.ToString(hashByte).ToLower().Replace("-", "");
        }

    }
}