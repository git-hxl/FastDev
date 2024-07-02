using FastDev;
using UnityEngine;

public class ResourceExample : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        await ResourceManager.Instance.LoadAllAssetBundle(Application.streamingAssetsPath + "/" + Utility.Platform.GetPlatformName(), OnProgress);

        Instantiate(ResourceManager.Instance.LoadAsset<GameObject>("prefab", "Assets/Arts/Prefab/Cube.prefab"));
    }

    void OnProgress(string name, float progress)
    {
        Debug.Log(name + ": " + progress);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
