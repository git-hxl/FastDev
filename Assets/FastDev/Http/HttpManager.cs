using System;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
namespace FastDev
{
    public class HttpManager : MonoSingleton<HttpManager>
    {

        public async UniTask Get(string url, Action<string> callback)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            try
            {
                var result = await request.SendWebRequest();
                callback?.Invoke(result.downloadHandler.text);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        public async UniTask Get(string url, Action<Texture2D> callback)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            try
            {
                var result = await request.SendWebRequest();
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(result.downloadHandler.data);
                callback?.Invoke(texture);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        public async UniTask Post(string url, WWWForm form, Action<string> callback)
        {
            UnityWebRequest request = UnityWebRequest.Post(url, form);
            try
            {
                var result = await request.SendWebRequest();
                callback?.Invoke(result.downloadHandler.text);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        public async UniTask Put(string url, byte[] bodyData, Action<string> callback)
        {
            UnityWebRequest request = UnityWebRequest.Put(url, bodyData);
            try
            {
                var result = await request.SendWebRequest();
                callback?.Invoke(result.downloadHandler.text);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        public async UniTask<bool> Download(string url, string path, Action<float> progress)
        {
            string fileName = url.GetFileName();
            path = path + "/" + fileName;
            UnityWebRequest request = UnityWebRequest.Get(url);
            request.downloadHandler = new DownloadHandlerFile(path);
            try
            {
                await request.SendWebRequest().ToUniTask(Progress.Create<float>(progress));
            }
            catch (Exception ex)
            {
                request.Abort();
                Debug.LogError(ex.Message);
                return false;
            }

            return true;
        }

    }
}