using UnityEngine;

namespace FastDev
{
    public class FirstPersonCameraSample : CameraBase
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
