using UnityEngine;

namespace FastDev
{
    public class CameraController : MonoBehaviour
    {
        protected float rotateXAngle;
        protected float rotateYAngle;

        protected Vector3 targetPos;
        protected Vector3 targetDir;
        protected Vector3 lerpTargetPos;
        protected Quaternion lerpTargetDir;

        public Vector3 Offset = new Vector3(0f, 0.25f, 0f);
        public float MaxXAngle = 60f;
        public float MaxZDistance = 10f;
        public float RotateSpeed = 200f;
        public float LerpSpeed = 20f;
        public float ScrollSpeed = 500f;


        public Transform Target;


        private float lastZDistance;
        private float curZDistance;


        private void OnEnable()
        {
            OnInit();
        }

        protected virtual void OnInit()
        {
            Cursor.lockState = CursorLockMode.Locked;
            rotateXAngle = 0;
            rotateYAngle = 0;
            curZDistance = 5;
            transform.forward = Target.forward;
        }

        private void Update()
        {
            UpdateRotate();
        }

        private void LateUpdate()
        {
            UpdatePos();
        }

        protected void UpdateRotate()
        {
            float x = Input.GetAxisRaw("Mouse X");
            float y = -Input.GetAxisRaw("Mouse Y");

            rotateXAngle += x * RotateSpeed * Time.deltaTime;
            rotateYAngle += y * RotateSpeed * Time.deltaTime;

            rotateYAngle = Mathf.Clamp(rotateYAngle, -MaxXAngle, MaxXAngle);

            targetDir = Vector3.forward;

            targetDir = Quaternion.AngleAxis(rotateYAngle, Vector3.right) * targetDir;
            targetDir = Quaternion.AngleAxis(rotateXAngle, Vector3.up) * targetDir;

            transform.forward = targetDir;
        }

        private void UpdatePos()
        {
            targetPos = Target.position + transform.TransformVector(Offset);

            float z = Input.GetAxis("Mouse ScrollWheel");

            if (z != 0)
            {
                curZDistance = curZDistance - z * ScrollSpeed * Time.deltaTime;
                curZDistance = Mathf.Clamp(curZDistance, 0, MaxZDistance);
                lastZDistance = curZDistance;
            }

            Ray ray = new Ray(targetPos, -transform.forward);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, lastZDistance))
            {
                if (raycastHit.transform != Target)
                {
                    float distance = Vector3.Distance(raycastHit.point, targetPos);
                    if (curZDistance > distance)
                    {
                        curZDistance -= LerpSpeed * Time.deltaTime;
                    }
                }
            }
            else if (curZDistance < lastZDistance)
            {
                curZDistance += LerpSpeed * Time.deltaTime;
            }

            targetPos = targetPos - targetDir * curZDistance;
            transform.position = targetPos;

        }
    }
}
