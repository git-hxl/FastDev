using UnityEngine;

namespace FastDev
{
    public class UILookToCamera : MonoBehaviour
    {
        private new Camera camera;
        // Start is called before the first frame update
        void Start()
        {
            camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            transform.forward = camera.transform.forward;
        }
    }
}
