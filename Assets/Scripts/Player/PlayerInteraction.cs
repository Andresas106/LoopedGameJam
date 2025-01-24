using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Configuración de Interacción")]
    public Camera playerCamera;              // Cámara personalizada (asignar manualmente)
    public float interactionDistance = 3f;    // Distancia de interacción
    public LayerMask interactableLayer;       // Capa de objetos interactuables
    public TextMeshProUGUI interactionText;   // Referencia al texto de la UI
    public TextMeshProUGUI pointerText;       // Referencia al texto de la 'O' (puntero)

    private InputManager inputManager;

    void Start()
    {
        if (playerCamera == null)
        {
            Debug.LogError("¡Por favor asigna la cámara del jugador en el inspector!");
        }

        if (interactionText == null || pointerText == null)
        {
            Debug.LogError("¡Por favor asigna el objeto de texto en el inspector!");
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

        // Crear un rayo desde la posición de la cámara hacia adelante
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // Dibujar el rayo para depuración
        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.red);

            // Si golpea un objeto interactuable, mostrar el texto
            if (hit.collider.CompareTag("Interactable"))
            {
                interactionText.gameObject.SetActive(true);  // Mostrar el mensaje
                pointerText.fontStyle = FontStyles.Bold;  // Poner el puntero en negrita

                // Detectar la tecla "E"
                if (inputManager.IsInteractPressed)
                {
                    Debug.Log("¡Interacción con: " + hit.collider.gameObject.name + "!");
                }
            }
        }
        else
        {
            interactionText.gameObject.SetActive(false);  // Ocultar el mensaje
            pointerText.fontStyle = FontStyles.Normal;  // Volver a la 'O' normal (no en negrita)
            Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.green);
        }
    }
}
