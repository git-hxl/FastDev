using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace FastDev
{
    public class WebRequestManager : Singleton<WebRequestManager>
    {
        public async UniTask<string> Get(string url)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            try
            {
                var result = await request.SendWebRequest();
                return result.downloadHandler.text;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                return "";
            }
        }

        public async UniTask<Sprite> GetSprite(string url)
        {
            UnityWebRequest request = UnityWebRequest.Get(url);
            try
            {
                var result = await request.SendWebRequest();
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(result.downloadHandler.data);
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                return sprite;
            }
            catch(Exception ex)
            {
                Debug.LogError(ex);
                return null;
            }
        }

        public async UniTask<string> Post(string url, WWWForm form)
        {
            UnityWebRequest request = UnityWebRequest.Post(url, form);
            try
            {
                var result = await request.SendWebRequest();
                return result.downloadHandler.text;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                return "";
            }
        }

        public async UniTask<string> Put(string url, byte[] bodyData)
        {
            UnityWebRequest request = UnityWebRequest.Put(url, bodyData);
            try
            {
                var result = await request.SendWebRequest();
                return result.downloadHandler.text;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                return "";
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
                UnityWebRequest result = await request.SendWebRequest().ToUniTask(Progress.Create<float>(progress));
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
                return false;
            }
        }

        public async UniTask<AssetBundle> DownloadAssetBundle(string url)
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url);
            try
            {
                var result = await request.SendWebRequest();
                return DownloadHandlerAssetBundle.GetContent(request);
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                return null;
            }
        }
    }
}