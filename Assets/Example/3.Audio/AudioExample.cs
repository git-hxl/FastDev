using System.Collections;
using System.Collections.Generic;
using FastDev;
using UnityEngine;

public class AudioExample : MonoBehaviour
{
    public string path;
    // Start is called before the first frame update
    void Start()
    {
        //AudioManager.Instance.PlayMusic("Assets/Example/Res/music.mp3");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlayClip(path);

        }
    }
    [ContextMenu("Clear")]
    public void ClearCache()
    {
        AudioManager.Instance.Dispose();
    }

}
