using UnityEngine;

namespace Framework
{
    public abstract class CameraBase : MonoBehaviour
    {
        protected float rotateXAngle;
        protected float rotateYAngle;

        protected Vector3 targetPos;
        protected Vector3 targetDir;
        protected Vector3 lerpTargetPos;
        protected Quaternion lerpTargetDir;

        public Vector3 Offset = new Vector3(0f, 0.25f, 0f);
        public float MaxXAngle = 60f;
        public float RotateSpeed = 200f;
        public float LerpSpeed = 20f;

        public Transform Target;

        private void OnEnable()
        {
            OnInit();
        }

        protected virtual void OnInit()
        {
            Cursor.lockState = CursorLockMode.Locked;
            rotateXAngle = 0;
            rotateYAngle = 0;
            transform.forward = Target.forward;
        }

        protected virtual void UpdateRotate()
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

        protected virtual void UpdatePos()
        {
            transform.position = targetPos;
        }
    }
}
