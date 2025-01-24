using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configuraci�n de Interacci�n")]
    public Camera playerCamera;              // C�mara personalizada (asignar manualmente)
    public float interactionDistance = 3f;    // Distancia de interacci�n
    public TextMeshProUGUI interactionText;   // Referencia al texto de la UI
    public TextMeshProUGUI pointerText;       // Referencia al texto de la 'O' (puntero)

    private InputManager inputManager;
    private bool hasInteracted = false;      // Variable para controlar si ya se ha interactuado

    void Start()
    {
        if (playerCamera == null)
        {
            Debug.LogError("�Por favor asigna la c�mara del jugador en el inspector!");
        }

        if (interactionText == null || pointerText == null)
        {
            Debug.LogError("�Por favor asigna el objeto de texto en el inspector!");
        }

        inputManager = FindObjectOfType<InputManager>();

        // Ocultar el texto al inicio
        interactionText.gameObject.SetActive(false);
        pointerText.fontStyle = FontStyles.Normal;  // Poner la 'O' en estilo normal
    }

    void Update()
    {
        DetectarInteraccion();
    }

    void DetectarInteraccion()
    {
        if (playerCamera == null) return;

        // Crear un rayo desde la posici�n de la c�mara hacia adelante
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // Dibujar el rayo para depuraci�n
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.red);

            // Si golpea un objeto interactuable, mostrar el texto
            if (hit.collider.CompareTag("Interactable"))
            {
                interactionText.gameObject.SetActive(true);  // Mostrar el mensaje
                pointerText.fontStyle = FontStyles.Bold;  // Poner el puntero en negrita

                // Detectar la tecla "E", pero solo permitir la interacci�n una vez
                if (inputManager.IsInteractPressed && !hasInteracted)
                {
                    Debug.Log("�Interacci�n con: " + hit.collider.gameObject.name + "!");
                    hasInteracted = true;  // Marcar que ya se ha interactuado
                }
            }
        }
        else
        {
            interactionText.gameObject.SetActive(false);  // Ocultar el mensaje
            pointerText.fontStyle = FontStyles.Normal;  // Volver a la 'O' normal (no en negrita)
            Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.green);
        }

        // Resetear la interacci�n cuando la tecla se suelte o el jugador deje de mirar el objeto
        if (!inputManager.IsInteractPressed)
        {
            hasInteracted = false;
        }
    }
}
