using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Cysharp.Threading.Tasks;
using ExcelDataReader;
using UnityEngine;
using UnityEngine.Networking;

namespace Bigger
{
    public static class FileUtil
    {
        public static async UniTask<string> ReadFile(string path)
        {
            if (path.Contains("://"))
            {
                string result = await ReadFromStreamingAssets(path);
                return result;
            }
            else
            {
                return ReadFromExternal(path);
            }
        }
        public static async UniTask<string> ReadFromStreamingAssets(string path)
        {
            UnityWebRequest web = UnityWebRequest.Get(path);
            var result = await web.SendWebRequest();
            return result.downloadHandler.text;
        }

        public static string ReadFromExternal(string path)
        {
            if (File.Exists(path))
                return File.ReadAllText(path);
            return "";
        }

        public static long GetFileLength(string path)
        {
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
            {
                return 0;
            }
            return file.Length;
        }

        public static Dictionary<string, string> LoadABManifest(AssetBundleManifest manifest)
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

        public static string GetMD5(string path)
        {
            var hash = MD5.Create();
            var stream = new FileStream(path, FileMode.Open);
            byte[] hashByte = hash.ComputeHash(stream);
            stream.Close();
            return BitConverter.ToString(hashByte).ToLower().Replace("-", "");
        }

        public static System.Data.DataTable ReadExcel(string path,int table = 0)
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    return result.Tables[table];
                }
            }
        }

    }
}