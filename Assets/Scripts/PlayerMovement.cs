using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    InputManager inputManager;
    CharacterController characterController;

    Vector2 currentMovementInput;
    Vector3 currentRunMovement;

    public Camera mainCamera;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    //variables de salto
    public float jumpHeight = 2.0f;  // Altura del salto
    public float gravity = -9.81f;   // Gravedad aplicada al personaje
    private bool isGrounded;         // Si el personaje está tocando el suelo
    private Vector3 velocity;

    Vector2 mouseDelta; // Almacena el input del ratón

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;
        handleMovement();
        RotatePlayer();
        handleJump();
    }

    private void RotatePlayer()
    {
        // Obtiene la entrada del mouse desde el Input Manager
        mouseDelta = inputManager.CurrentMouseDelta;

        float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

        // Rotación vertical de la cámara (eje X)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limitar rotación vertical
        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotación horizontal del personaje (eje Y)
        transform.Rotate(Vector3.up * mouseX);
    }

    private void handleMovement()
        {
        // Obtiene la entrada de movimiento actual desde el InputManager
        Vector2 currentMovementInput = inputManager.CurrentMovementInput;

        // Convierte la entrada del movimiento para obtener la dirección en el espacio del mundo
        Vector3 moveDirection = transform.TransformDirection(new Vector3(currentMovementInput.x, 0, currentMovementInput.y));

        // Asignación de movimiento, modificada con la velocidad de movimiento para caminar o correr
        if (inputManager.IsRunPressed)
        {
            characterController.Move(moveDirection * 6.0f * Time.deltaTime); // Movimiento más rápido al correr
        }
        else
        {
            characterController.Move(moveDirection * 3.0f * Time.deltaTime); // Movimiento normal al caminar
        }
    }

    private void handleJump()
    {
        // Si estamos en el suelo y estamos cayendo, detener la caída para evitar "caídas flotantes"
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Ajuste de "amortiguamiento" en el suelo para evitar flotación o rebotes
        }

        // Comprobamos si se ha presionado el salto
        if (inputManager.IsJumpPressed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Velocidad inicial calculada para el salto
        }


        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;  // Aplica la gravedad solo después de empezar a caer
        }
        // Aplica la gravedad solo si el personaje no está en el suelo o saltando


        // Movemos al personaje
        characterController.Move(velocity * Time.deltaTime);
    }
}