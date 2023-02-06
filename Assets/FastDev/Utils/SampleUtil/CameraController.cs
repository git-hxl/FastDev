using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        public Transform Target;
        public Vector3 Offset;

        public float RotateSpeed;
        public float ScrollSpeed;
        public float LerpSpeed;
        public float MaxZDistance;
        public float MaxXAngle;

        public bool Raycast;
        public float WallThickness;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Target == null)
                return;

            TPController();
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

            Offset.z = Mathf.Clamp(Offset.z + z * ScrollSpeed, MaxZDistance, 0);

            Vector3 targetPos = Target.position + transform.TransformVector(Offset);

            if (Raycast)
            {
                RaycastHit raycastHit;
                Vector3 dir = (Target.position - targetPos).normalized;
                Ray ray = new Ray(targetPos, dir);
                if (Physics.Raycast(ray, out raycastHit))
                {
                    if (raycastHit.transform != Target)
                    {
                        targetPos = raycastHit.point + dir * WallThickness;
                    }
                }
            }

            transform.position = Vector3.Lerp(transform.position, targetPos, LerpSpeed * Time.deltaTime);

        }
    }
}
