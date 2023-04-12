using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    public class SampleLoadAsset : MonoBehaviour
    {
        // Start is called before the first frame update
        async void Start()
        {
            //await AssetManager.Instance.LoadAssetBundleAsync("Assets/StreamingAssets/StandaloneWindows/prefab", (progress) => { Debug.Log(progress); });

            AssetBundleConfig config = AssetManager.Instance.LoadConfig();

            Debug.Log(config.DateTime);

            GameObject cube = AssetManager.Instance.LoadAsset<GameObject>("prefab", "Assets/FastDev/1.AssetManager/Sample/Cube.prefab");
            Instantiate(cube);

            GameObject cube2 = await AssetManager.Instance.LoadAssetAsync<GameObject>("prefab", "Assets/FastDev/1.AssetManager/Sample/Cube.prefab");
            cube2.name = "async cube";
            Instantiate(cube2);
        }
    }
}
