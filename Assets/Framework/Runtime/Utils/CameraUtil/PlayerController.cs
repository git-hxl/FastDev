using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Camera cacheCamera;
    private bool isGround;
    private float velocitY;

    private Vector3 inputDir;

    public float MoveSpeed = 2.5f;
    public float JumpHeight = 2.5f;
    public float GravityFactor = 3f;
    public float TurnSpeed = 10f;
    public bool LockDir;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        cacheCamera = Camera.main;
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        isGround = characterController.isGrounded;

        if (isGround && velocitY < 0)
        {
            velocitY = 0f;
        }

        inputDir = new Vector3(x, 0, z).normalized;

        characterController.Move(transform.TransformDirection(inputDir) * MoveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            velocitY += Mathf.Sqrt(JumpHeight * -3.0f * Physics.gravity.y);
        }

        velocitY += Physics.gravity.y * GravityFactor * Time.deltaTime;
        characterController.Move(Vector3.up * velocitY * Time.deltaTime);
    }

    private void LateUpdate()
    {
        if (LockDir)
        {
            transform.eulerAngles = new Vector3(0, cacheCamera.transform.eulerAngles.y, 0);
        }
        else if (inputDir != Vector3.zero)
        {
            transform.eulerAngles = new Vector3(0, cacheCamera.transform.eulerAngles.y, 0);
        }
    }
}