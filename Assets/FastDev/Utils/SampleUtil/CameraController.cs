using UnityEngine;

namespace FastDev
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        public Transform Target;
        public Vector3 TPOffset;
        public Vector3 FPOffset;

        public CameraMode Mode;

        public float RotateSpeed;
        public float ScrollSpeed;
        public float LerpSpeed;
        public float MaxZDistance;
        public float MaxXAngle;

        public bool Raycast;
        public float WallThickness;

        private void Start()
        {
            if (Target == null)
                return;
            transform.forward = Target.forward;
        }

        void LateUpdate()
        {
            if (Target == null)
                return;

            switch (Mode)
            {
                case CameraMode.TP:
                    TPController();
                    break;
                case CameraMode.FP:
                    FPController();
                    break;
            }

        }


        void TPController()
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                float xAngle = transform.eulerAngles.x;
                if (xAngle > 180)
                {
                    xAngle -= 360;
                }

                float x = Input.GetAxis("Mouse X");
                float y = -Input.GetAxis("Mouse Y");
                if (x != 0)
                    transform.RotateAround(Target.position, Vector3.up, x * RotateSpeed);
                if (y != 0 && ((xAngle < MaxXAngle && y > 0) || (xAngle > -MaxXAngle && y < 0)))
                    transform.RotateAround(Target.position, transform.right, y * RotateSpeed);
            }

            float z = Input.GetAxis("Mouse ScrollWheel");

            TPOffset.z = Mathf.Clamp(TPOffset.z + z * ScrollSpeed, MaxZDistance, 0);

            Vector3 targetPos = Target.position + transform.TransformVector(TPOffset);

            if (Raycast)
            {
                RaycastHit raycastHit;
                Vector3 dir = (targetPos - Target.position).normalized;
                float maxDistance = Vector3.Distance(targetPos, Target.position);
                Ray ray = new Ray(Target.position, dir);
                if (Physics.Raycast(ray, out raycastHit, maxDistance))
                {
                    if (raycastHit.transform != Target)
                    {
                        targetPos = raycastHit.point + -dir * WallThickness;
                    }
                }
            }

            transform.position = Vector3.Lerp(transform.position, targetPos, LerpSpeed * Time.deltaTime);
        }

        void FPController()
        {
            if (Cursor.lockState != CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.Locked;

            float xAngle = transform.eulerAngles.x;
            if (xAngle > 180)
            {
                xAngle -= 360;
            }

            float x = Input.GetAxis("Mouse X");
            float y = -Input.GetAxis("Mouse Y");
            if (x != 0)
            {
                transform.Rotate(Vector3.up, x * RotateSpeed, Space.World);
            }
            if (y != 0 && ((xAngle < MaxXAngle && y > 0) || (xAngle > -MaxXAngle && y < 0)))
                transform.Rotate(Vector3.right, y * RotateSpeed);

            Vector3 targetPos = Target.position + Target.TransformVector(FPOffset);
            transform.position = targetPos;
            //第一人称视角人物转向受摄像机控制
            Target.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }
    }
}
