using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private new Camera camera;
    private bool isGround;
    private float velocitY;

    public float MoveSpeed = 2.5f;
    public float RotateSpeed = 10f;
    public float JumpHeight = 2.5f;
    public float GravityFactor = 3f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        camera = Camera.main;
    }

    private void Update()
    {
        isGround = characterController.isGrounded;
        if (isGround && velocitY < 0)
        {
            velocitY = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Quaternion targetAngle = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, RotateSpeed * Time.deltaTime);
        characterController.Move(transform.TransformDirection(move) * MoveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            velocitY += Mathf.Sqrt(JumpHeight * -3.0f * Physics.gravity.y);
        }

        velocitY += Physics.gravity.y * GravityFactor * Time.deltaTime;
        characterController.Move(Vector3.up * velocitY * Time.deltaTime);
    }
}
