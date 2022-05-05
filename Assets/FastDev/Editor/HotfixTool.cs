using System.IO;
using UnityEditor;
using UnityEngine;

namespace FastDev.Editor
{
    public class HotfixTool
    {
        public const string dllPath = "./HotFixProject/HotFixProject/bin/Release/net5.0/HotfixProject.dll";
        public const string pdbPath = "./HotFixProject/HotFixProject/bin/Release/net5.0/HotfixProject.pdb";

        [MenuItem("FastDev/更新热补丁文件", true)]
        private static bool UpdateHotfixDllVa()
        {
            return File.Exists(dllPath);
        }
        [MenuItem("FastDev/更新热补丁文件", false, 0)]
        public static void UpdateHotfixDll()
        {
            //更新热补丁dll

            string dir = Application.dataPath + "/Hotfix";
            string destDllPath = dir + "/Hotfix.dll.bytes";
            string destPdbPath = dir + "/Hotfix.pdb.bytes";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            using (FileStream write = new FileStream(destDllPath, FileMode.Create, FileAccess.ReadWrite))
            {
                using (FileStream read = new FileStream(dllPath, FileMode.Open, FileAccess.Read))
                {
                    byte[] data = new byte[read.Length];
                    read.Read(data, 0, data.Length);
                    write.Write(data, 0, data.Length);
                }
            }

            using (FileStream write = new FileStream(destPdbPath, FileMode.Create, FileAccess.ReadWrite))
            {
                using (FileStream read = new FileStream(pdbPath, FileMode.Open, FileAccess.Read))
                {
                    byte[] data = new byte[read.Length];
                    read.Read(data, 0, data.Length);
                    write.Write(data, 0, data.Length);
                }
            }
            AssetDatabase.Refresh();
            Debug.Log("Update Hotfix.dll");
        }

    }
}