using FastDev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleLoadAsset : MonoBehaviour
{
    // Start is called before the first frame update
    async void Awake()
    {
        //await AssetManager.Instance.LoadAssetBundleAsync("Assets/StreamingAssets/StandaloneWindows/prefab", (progress) => { Debug.Log(progress); });

        ResourceConfig config = AssetManager.Instance.LoadConfig();

        Debug.Log(config.DateTime);

        GameObject cube = AssetManager.Instance.LoadAsset<GameObject>("prefab", "Assets/GameFramework/Sample/Asset/Cube.prefab");
        Instantiate(cube);

        GameObject cube2 = await AssetManager.Instance.LoadAssetAsync<GameObject>("prefab", "Assets/GameFramework/Sample/Asset/Cube.prefab");
        cube2.name = "async cube";
        Instantiate(cube2);
    }
}