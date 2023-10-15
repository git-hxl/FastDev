using UnityEngine;

namespace FastDev
{
    public class UILookToCamera : MonoBehaviour
    {
        private Camera cacheCamera;
        // Start is called before the first frame update
        void Start()
        {
            cacheCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            transform.forward = cacheCamera.transform.forward;
        }
    }
}
