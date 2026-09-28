using UnityEngine;
using TinyEngineers.Input;

namespace TinyEngineers.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float rotationSpeed = 10f;
        
        private Rigidbody rb;
        private Vector3 movementInput;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.interpolation = RigidbodyInterpolation.Interpolate;
        }

        private void Update()
        {
            Vector2 rawInput = InputManager.Instance != null ? InputManager.Instance.JoystickInput : Vector2.zero;
            // Isometric mapping: Up joystick goes Forward in world
            movementInput = new Vector3(rawInput.x, 0f, rawInput.y);
        }

        private void FixedUpdate()
        {
            MovePlayer();
            RotatePlayer();
        }

        private void MovePlayer()
        {
            Vector3 movement = movementInput * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }

        private void RotatePlayer()
        {
            if (movementInput != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementInput);
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
            }
        }
        
        public bool IsMoving => movementInput.magnitude > 0.1f;
    }
}
