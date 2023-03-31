using UnityEngine;

namespace FastDev
{
    public class FirstPersonCamera : MonoBehaviour
    {
        public Transform Target;
        public Vector3 Offset = new Vector3(0f, 0f, 0f);

        public float RotateSpeed = 200f;
        public float MaxXAngle = 50f;

        private void OnEnable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            transform.forward = Target.forward;
        }

        private void Update()
        {
            float x = Input.GetAxis("Mouse X");
            float y = -Input.GetAxis("Mouse Y");

            transform.Rotate(Vector3.up, x * RotateSpeed * Time.deltaTime, Space.World);

            float xAngle = transform.eulerAngles.x;

            if (xAngle > 180)
                xAngle -= 360;

            if ((xAngle < MaxXAngle && y > 0) || (xAngle > -MaxXAngle && y < 0))
                transform.Rotate(Vector3.right, y * RotateSpeed * Time.deltaTime, Space.Self);

        }

        private void LateUpdate()
        {
            Vector3 targetPos = Target.position + Target.TransformVector(Offset);
            transform.position = targetPos;
        }
    }
}
