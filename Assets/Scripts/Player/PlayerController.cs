using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputManager inputManager;
    CharacterController characterController;
    AudioSource audioSource;

    Vector2 currentMovementInput;
    Vector3 currentRunMovement;

    public Camera mainCamera;
    public float mouseSensitivity = 100f;
    private float xRotation = 0f;

    public float jumpHeight = 2.0f;
    public float gravity = -9.81f;
    private bool isGrounded;
    private Vector3 velocity;

    Vector2 mouseDelta;

    public GameObject ecoPrefab;
    public Transform respawnPoint;

    public GameObject spawnEffect;
    public GameObject deathEffect;

    public AudioClip deathSound;
    public AudioClip delayedSound; // Nuevo sonido que sonará después de un tiempo
    public float delayedSoundDelay = 2f; // Tiempo en segundos antes de que suene el segundo audio

    private bool isDead = false;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (spawnEffect != null) spawnEffect.SetActive(false);
        if (deathEffect != null) deathEffect.SetActive(false);
    }

    void Update()
    {
        if (isDead) return;
        isGrounded = characterController.isGrounded;
        handleMovement();
        RotatePlayer();
        handleJump();

        if (inputManager.IsDiePressed)
        {
            Die();
        }
    }

    private void RotatePlayer()
    {
        mouseDelta = inputManager.CurrentMouseDelta;

        float mouseX = mouseDelta.x * mouseSensitivity;
        float mouseY = mouseDelta.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    private void handleMovement()
    {
        Vector2 currentMovementInput = inputManager.CurrentMovementInput;
        Vector3 moveDirection = transform.TransformDirection(new Vector3(currentMovementInput.x, 0, currentMovementInput.y));

        if (inputManager.IsRunPressed)
        {
            characterController.Move(moveDirection * 6.0f * Time.deltaTime);
        }
        else
        {
            characterController.Move(moveDirection * 3.0f * Time.deltaTime);
        }
    }

    private void handleJump()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (inputManager.IsJumpPressed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, characterController.height * 0.55f))
        {
            if (velocity.y > 0)
            {
                velocity.y = 0f;
                Debug.Log("Chocando con el techo");
            }
        }

        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        velocity.y = Mathf.Max(velocity.y, -20f);

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

        if (deathEffect != null)
        {
            deathEffect.transform.position = transform.position;
            deathEffect.SetActive(true);
        }

        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // Si hay un sonido retrasado configurado, iniciar la corutina para reproducirlo
        if (delayedSound != null)
        {
            StartCoroutine(PlayDelayedSound());
        }

        Invoke(nameof(Respawn), 1f);
    }

    IEnumerator PlayDelayedSound()
    {
        yield return new WaitForSeconds(delayedSoundDelay);
        if (audioSource != null && delayedSound != null)
        {
            audioSource.PlayOneShot(delayedSound);
        }
    }

    void Respawn()
    {
        characterController.enabled = false;

        transform.position = respawnPoint.position;
        isDead = false;

        if (spawnEffect != null)
        {
            spawnEffect.SetActive(true);
            Invoke(nameof(HideSpawnEffect), 2f);
        }

        characterController.enabled = true;
    }

    private void HideSpawnEffect()
    {
        if (spawnEffect != null)
        {
            spawnEffect.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Matable"))
        {
            Die();
        }
    }
}
