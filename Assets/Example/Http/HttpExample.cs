using UnityEngine;
using FastDev;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
public class HttpExample : MonoBehaviour
{
    public Image image;
    public string urlImage;
    public string urlTxt;
    public string urlMP3;
    // Start is called before the first frame update
    private void Start()
    {
        HttpManager.Instance.Get(urlTxt, OnTxtDone).Forget();

        HttpManager.Instance.Get(urlImage, OnImageDone).Forget();

        HttpManager.Instance.Download(urlMP3, Application.persistentDataPath, OnMp3DownProcess).Forget();
    }


    void OnTxtDone(string txt)
    {
        Debug.Log(txt);
    }

    void OnImageDone(Texture2D texture2D)
    {

        image.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);
    }

    void OnMp3DownProcess(float process)
    {
        Debug.Log("下载进度：" + process);
    }
}
