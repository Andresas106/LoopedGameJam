using UnityEngine;

/// <summary>
/// Detecta qué objeto está mirando el jugador e interactúa con él al presionar "E".
/// </summary>
public class InteraccionPlayer : MonoBehaviour
{
    [Header("Configuración")]
    public Camera playerCamera;        // Cámara del jugador
    public float raycastDistance = 5f; // Distancia máxima del raycast

    private InputManager inputManager; // Sistema de entrada

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        DetectarYActivarObjeto();
    }

    /// <summary>
    /// Lanza un raycast para detectar el objeto interactuable y activa su interacción.
    /// </summary>
    void DetectarYActivarObjeto()
    {
        Ray rayo = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(rayo, out hit, raycastDistance))
        {
            IInteractuable objeto = hit.collider.GetComponent<IInteractuable>();

            if (objeto != null && inputManager.IsInteractPressed)
            {
                objeto.Interactuar();
            }
        }
    }
}
