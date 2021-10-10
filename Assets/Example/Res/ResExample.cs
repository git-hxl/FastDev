using System.Collections;
using System.Collections.Generic;
using Bigger;
using UnityEngine;

public class ResExample : MonoBehaviour
{
    public Sprite sprite;
    public Texture texture;
    public Texture2D texture2D;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ResManager.Instance.LoadAsset<GameObject>("prefab", "Assets/Example/Pool/Cube.prefab"));
        sprite = ResManager.Instance.LoadAsset<Sprite>("texture2d", "Assets/Example/Res/1.jpg");
        texture = ResManager.Instance.LoadAsset<Texture>("texture2d", "Assets/Example/Res/1.jpg");
        texture2D = ResManager.Instance.LoadAsset<Texture2D>("texture2d", "Assets/Example/Res/1.jpg");
    }
}
