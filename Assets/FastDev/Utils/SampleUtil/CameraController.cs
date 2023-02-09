using UnityEngine;

namespace FastDev
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        public Transform Target;
        public Vector3 TPOffset = new Vector3(0, 2, -8);
        public Vector3 FPOffset = new Vector3(0, 1, 0);

        public CameraMode Mode = CameraMode.FP;

        public float RotateSpeed = 200f;
        public float ScrollSpeed = 200f;
        public float LerpSpeed = 5f;
        public float MaxZDistance = -20f;
        public float MaxXAngle = 50f;

        public bool Raycast = true;
        public float WallThickness = 1f;

        private CameraMode curMode;

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

            if (curMode != Mode)
            {
                transform.forward = Target.forward;
            }

            curMode = Mode;

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
                    transform.RotateAround(Target.position, Vector3.up, x * RotateSpeed * Time.deltaTime);
                if (y != 0 && ((xAngle < MaxXAngle && y > 0) || (xAngle > -MaxXAngle && y < 0)))
                    transform.RotateAround(Target.position, transform.right, y * RotateSpeed * Time.deltaTime);
            }

            float z = Input.GetAxis("Mouse ScrollWheel");

            TPOffset.z = Mathf.Clamp(TPOffset.z + z * ScrollSpeed * Time.deltaTime, MaxZDistance, 0);

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
            float y = -Input.GetAxis("Mouse Y");
            if (y != 0 && ((xAngle < MaxXAngle && y > 0) || (xAngle > -MaxXAngle && y < 0)))
                transform.Rotate(Vector3.right, y * RotateSpeed * Time.deltaTime);

            Vector3 targetPos = Target.position + Target.TransformVector(FPOffset);
            transform.position = targetPos;

            //第一人称视角受目标控制
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, Target.transform.eulerAngles.y, 0);
        }
    }
}
