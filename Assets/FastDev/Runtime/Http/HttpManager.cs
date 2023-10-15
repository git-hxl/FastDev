using System;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Text;
using System.IO;

namespace FastDev
{
    public class HttpManager : Singleton<HttpManager>
    {

        public async UniTask<string> GetTxt(string url)
        {
            var data = await Get(url);
            if (data != null)
            {
                return Encoding.UTF8.GetString(data);
            }
            return "";
        }

        public async UniTask<Texture2D> GetTexture2D(string url)
        {
            var data = await Get(url);
            if (data != null)
            {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(data);
                return texture;
            }
            return null;
        }

        public async UniTask<byte[]> Get(string url)
        {
            try
            {
                UnityWebRequest request = UnityWebRequest.Get(url);
                var result = await request.SendWebRequest();
                return result.downloadHandler.data;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                return null;
            }
        }
        public async UniTask<byte[]> Post(string url, WWWForm form)
        {
            try
            {
                UnityWebRequest request = UnityWebRequest.Post(url, form);
                var result = await request.SendWebRequest();
                return result.downloadHandler.data;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                return null;
            }
        }

        public async UniTask<byte[]> Put(string url, byte[] bodyData)
        {

            try
            {
                UnityWebRequest request = UnityWebRequest.Put(url, bodyData);
                var result = await request.SendWebRequest();
                return result.downloadHandler.data;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                return null;
            }
        }

        public async UniTask<bool> Download(string url, string dir, Action<string, float> progress)
        {
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(url);
                string path = dir + "/" + fileName;
                using (UnityWebRequest request = UnityWebRequest.Get(url))
                {
                    request.downloadHandler = new DownloadHandlerFile(path);
                    await request.SendWebRequest().ToUniTask(Progress.Create<float>((process) => progress(fileName, process)));
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                return false;
            }

            return true;
        }

    }
}