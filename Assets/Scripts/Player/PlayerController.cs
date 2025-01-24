using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    public GameObject ecoPrefab;
    public Transform respawnPoint;

    private bool isDead = false;

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

        if (isDead) return;
        isGrounded = characterController.isGrounded;
        handleMovement();
        RotatePlayer();
        handleJump();

        //muerte manual por tecla
        if (inputManager.IsDiePressed)
        {
            Die();
        }

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
        // Si estamos en el suelo, reseteamos la velocidad vertical para evitar acumulaciones
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // Valor pequeño negativo para asegurar que no quede flotando
        }

        // Comprobamos si estamos quietos y si se presiona el salto
        if (inputManager.IsJumpPressed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Inicializamos la velocidad de salto
        }

        // Raycast para detectar si estamos cerca del techo
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, characterController.height * 0.55f))
        {
            if (velocity.y > 0)  // Si está subiendo (saltando)
            {
                velocity.y = 0f;  // Detenemos el salto al chocar con el techo
                Debug.Log("Chocando con el techo");
            }
        }

        // Aplicar gravedad de manera continua
        if (!isGrounded)  // Si no estamos en el suelo, aplicar gravedad
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // Límite máximo de velocidad de caída para evitar aceleración infinita
        velocity.y = Mathf.Max(velocity.y, -20f);

        // Aplicar movimiento en Y manteniendo X y Z independientes
        Vector3 move = new Vector3(0, velocity.y, 0) * Time.deltaTime;
        characterController.Move(move);
    }


    public void Die()
    {
        if (isDead) return;
        isDead = true;

        if (ecoPrefab != null)
        {
            Instantiate(ecoPrefab, transform.position, Quaternion.identity);
        }

        Invoke(nameof(Respawn), 1f);
    }

    void Respawn()
    {
        characterController.enabled = false;
        transform.position = respawnPoint.position;
        isDead = false;
        characterController.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Matable"))
        {
            Die();
        }
    }
}
