using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastDev
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        private CharacterController characterController;
        private Camera camera;
        private bool isGround;
        private float velocitY;

        public float MoveSpeed;
        public float TurnSpeed;
        public float JumpHeight;

        public float GravityFactor = 1f;
        // Start is called before the first frame update
        void Start()
        {
            characterController = GetComponent<CharacterController>();
            camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            isGround = characterController.isGrounded;
            if (isGround && velocitY < 0)
            {
                velocitY = 0f;
            }

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (move != Vector3.zero && camera != null)
            {
                Quaternion targetAngle = Quaternion.Euler(0, camera.transform.eulerAngles.y, 0);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetAngle, TurnSpeed * Time.deltaTime);
                characterController.Move(transform.forward * Time.deltaTime * MoveSpeed);
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                velocitY += Mathf.Sqrt(JumpHeight * -3.0f * Physics.gravity.y);
            }

            velocitY += Physics.gravity.y * GravityFactor * Time.deltaTime;
            characterController.Move(Vector3.up * velocitY * Time.deltaTime);
        }
    }
}
