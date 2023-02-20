using UnityEngine;
public class FirstPersonCamera : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset = new Vector3(0f, 0f, 0f);

    public float RotateSpeed = 200f;
    public float MaxXAngle = 50f;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.forward = Target.forward;
    }

    private void Update()
    {
        float x = Input.GetAxis("Mouse X");
        float y = -Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, x * RotateSpeed * Time.deltaTime, Space.World);

        transform.Rotate(Vector3.right, y * RotateSpeed * Time.deltaTime, Space.Self);

        float xAngle = transform.eulerAngles.x;
        if (xAngle > MaxXAngle && xAngle < 180)
        {
            xAngle = MaxXAngle;
        }
        if (xAngle < 360 - MaxXAngle && xAngle > 180)
        {
            xAngle = 360 - MaxXAngle;
        }
        transform.rotation = Quaternion.Euler(xAngle, transform.eulerAngles.y, 0);

        //第一人称人物旋转跟随摄像机旋转
        Target.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
    }

    private void LateUpdate()
    {
        Vector3 targetPos = Target.position + Target.TransformVector(Offset);
        transform.position = targetPos;
    }
}