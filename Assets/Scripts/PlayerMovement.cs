using UnityEngine;

/*
 * PlayerMovement.cs
 * 
 * This script handles the movement and camera control for a first-person player.
 * - Movement using keyboard (WASD)
 * - Camera rotation with mouse
 * - Jumping with space key
 * 
 */

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;              // Player movement speed
    public float jumpHeight = 2f;          // Jump force
    public float gravity = -9.81f;         // Gravity force

    [Header("Mouse Settings")]
    public float mouseSensitivity = 100f;  // Sensitivity for mouse movement
    public Transform playerCamera;         // Reference to the camera

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;

    void Start()
    {
        // Hide and lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    void MovePlayer()
    {
        // Get input from keyboard
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Convert input to movement direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Apply movement
        controller.Move(move * speed * Time.deltaTime);

        // Apply gravity
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Keep grounded
        }

        // Jump logic
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void RotatePlayer()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate player around Y axis
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera around X axis (clamp to prevent flipping)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
