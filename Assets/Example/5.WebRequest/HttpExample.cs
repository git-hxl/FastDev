using UnityEngine;
using FastDev;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
public class HttpExample : MonoBehaviour
{
    public string url = "https://m10.music.126.net/20210524172221/0486b5db89c8eb7fc72ce6d8ef2f18ba/ymusic/0f53/045e/055c/89b330c1e634def43efc26e095bafb25.mp3";
    float progress;
    // Start is called before the first frame update
    async UniTaskVoid Start()
    {
        string fileName = url.GetFileName();
        bool result = await WebRequestManager.Instance.Download(url, "./Download", Progress);
        Debug.Log(fileName + ":下载:" + result);
    }


    private void Progress(float value)
    {
        Debug.Log(value * 100 + "%");
    }
}
