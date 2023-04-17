using System;
using UnityEngine;
namespace FastDev
{
    public class ThirdPersonCamera : CameraBase
    {
        
        private float setZDistance;
        private float autoSetZDistance;

        public float MaxZDistance = 10f;
        public float ScrollSpeed = 500f;

        protected override void OnInit()
        {
            base.OnInit();
            setZDistance = MaxZDistance;
            autoSetZDistance = setZDistance / 2f;
        }

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
            float z = Input.GetAxis("Mouse ScrollWheel");

            if (z != 0)
            {
                setZDistance = setZDistance - z * ScrollSpeed * Time.deltaTime;
                setZDistance = Mathf.Clamp(setZDistance, 0, MaxZDistance);
                autoSetZDistance = setZDistance;
            }

            targetPos = Target.position + transform.TransformVector(Offset);

            Ray ray = new Ray(targetPos, -transform.forward);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, setZDistance))
            {
                if (raycastHit.transform != Target)
                {
                    float distance = Vector3.Distance(raycastHit.point, targetPos);
                    if (autoSetZDistance > distance)
                    {
                        autoSetZDistance -= LerpSpeed * Time.deltaTime;
                    }
                }
            }
            else if (autoSetZDistance < setZDistance)
            {
                autoSetZDistance += LerpSpeed * Time.deltaTime;
            }

            targetPos = targetPos - targetDir * autoSetZDistance;
            transform.position = targetPos;
        }

        
    }
}