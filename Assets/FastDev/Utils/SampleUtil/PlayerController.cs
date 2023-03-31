using UnityEngine;

namespace FastDev
{
    public class PlayerController : MonoBehaviour
    {
        private CharacterController characterController;
        private new Camera camera;
        private bool isGround;
        private float velocitY;

        public float MoveSpeed = 2.5f;
        public float JumpHeight = 2.5f;
        public float GravityFactor = 3f;
        public float TurnSpeed = 10f;

        private void Start()
        {
            characterController = GetComponent<CharacterController>();
            camera = Camera.main;
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

            Vector3 moveDir = new Vector3(x, 0, z).normalized;
            characterController.Move(transform.TransformDirection(moveDir) * MoveSpeed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                velocitY += Mathf.Sqrt(JumpHeight * -3.0f * Physics.gravity.y);
            }

            velocitY += Physics.gravity.y * GravityFactor * Time.deltaTime;
            characterController.Move(Vector3.up * velocitY * Time.deltaTime);
        }

        private void LateUpdate()
        {
            Quaternion targetRotation = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
            if (transform.rotation != targetRotation)
            {
                transform.rotation = targetRotation;//Quaternion.Slerp(transform.rotation, targetRotation, TurnSpeed * Time.deltaTime);
            }
        }
    }

}
