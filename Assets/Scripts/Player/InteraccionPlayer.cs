using UnityEngine;

/// <summary>
/// Detecta qu� objeto est� mirando el jugador e interact�a con �l al presionar "E".
/// </summary>
public class InteraccionPlayer : MonoBehaviour
{
    [Header("Configuraci�n")]
    public Camera playerCamera;        // C�mara del jugador
    public float raycastDistance = 5f; // Distancia m�xima del raycast

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
    /// Lanza un raycast para detectar el objeto interactuable y activa su interacci�n.
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
