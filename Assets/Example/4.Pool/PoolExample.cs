using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FastDev;
public class PoolExample : MonoBehaviour
{
    public Button btAllocate;
    public Button btRecycle;
    public string resPath;

    private List<GameObject> gameObjects = new List<GameObject>();
    private void Start() {

        btAllocate.onClick.AddListener(() =>
        {
            gameObjects.Add(ObjectPool.Instance.Allocate(resPath));
        });

        btRecycle.onClick.AddListener(() =>
        {
            if(gameObjects.Count > 0)
            {
                ObjectPool.Instance.Recycle(gameObjects[gameObjects.Count - 1]);
                gameObjects.Remove(gameObjects[gameObjects.Count - 1]);
            }

        });

    }
}
