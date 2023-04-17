using UnityEngine;

namespace FastDev
{
    public class FirstPersonCamera : CameraBase
    {
        private void Update()
        {
            UpdateRotate();
        }

        private void LateUpdate()
        {
            UpdatePos();
        }

        protected override void UpdatePos()
        {
            targetPos = Target.position + Target.TransformVector(Offset);
            transform.position = targetPos;
        }
    }
}
