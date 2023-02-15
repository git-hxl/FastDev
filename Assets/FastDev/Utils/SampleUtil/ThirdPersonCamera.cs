using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset = new Vector3(0f, 0.25f, -1f);

    public float RotateSpeed = 200f;
    public float LerpSpeed = 5f;
    public float MaxXAngle = 50f;

    public float ScrollSpeed = 200f;
    public float MaxZDistance = -10f;
    public bool Raycast = true;
    public float WallThickness = 1f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.rotation = Quaternion.Euler(45f, 0f, 0f);
    }

    private void LateUpdate()
    {
        float xAngle = transform.eulerAngles.x;
        if (xAngle > 180)
            xAngle -= 360;

        float x = Input.GetAxis("Mouse X");
        float y = -Input.GetAxis("Mouse Y");
        if (x != 0)
            transform.RotateAround(Target.position, Vector3.up, x * RotateSpeed * Time.deltaTime);
        if (y != 0 && ((xAngle < MaxXAngle && y > 0) || (xAngle > -MaxXAngle && y < 0)))
            transform.RotateAround(Target.position, transform.right, y * RotateSpeed * Time.deltaTime);

        float z = Input.GetAxis("Mouse ScrollWheel");

        Offset.z = Mathf.Clamp(Offset.z + z * ScrollSpeed * Time.deltaTime, MaxZDistance, 0);

        Vector3 targetPos = Target.position + transform.TransformVector(Offset);

        if (Raycast)
        {
            RaycastHit raycastHit;
            Vector3 dir = (targetPos - Target.position).normalized;
            float maxDistance = Vector3.Distance(targetPos, Target.position);
            Ray ray = new Ray(Target.position, dir);
            if (Physics.Raycast(ray, out raycastHit, maxDistance))
            {
                if (raycastHit.collider.gameObject.tag != "Player")
                {
                    targetPos = raycastHit.point + -dir * WallThickness;
                }
            }
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, LerpSpeed * Time.deltaTime);
    }
}
