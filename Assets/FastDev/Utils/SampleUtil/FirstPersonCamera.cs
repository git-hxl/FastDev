using UnityEngine;
public class FirstPersonCamera : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset = new Vector3(0f, 0f, 0f);

    public float RotateSpeed = 200f;
    public float LerpSpeed = 20f;
    public float MaxXAngle = 50f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.forward = Target.forward;
    }

    private void LateUpdate()
    {
        float xAngle = transform.eulerAngles.x;
        if (xAngle > 180)
            xAngle -= 360;

        float x = Input.GetAxis("Mouse X");
        float y = -Input.GetAxis("Mouse Y");
        if (x != 0)
            transform.Rotate(Vector3.up, x * RotateSpeed * Time.deltaTime, Space.World);
        if (y != 0 && ((xAngle < MaxXAngle && y > 0) || (xAngle > -MaxXAngle && y < 0)))
            transform.Rotate(Vector3.right, y * RotateSpeed * Time.deltaTime, Space.Self);

        Vector3 targetPos = Target.position + Target.TransformVector(Offset);

        transform.position = Vector3.Lerp(transform.position, targetPos, LerpSpeed * Time.deltaTime);
    }
}