using UnityEngine;

/// <summary>
/// Detecta qué objeto está mirando el jugador e interactúa con él al presionar "E".
/// </summary>
public class InteraccionPlayer777 : MonoBehaviour
{
    [Header("Configuración")]
    public Camera playerCamera;        // Cámara del jugador
    public float raycastDistance = 5f; // Distancia máxima del raycast

    private InputManager inputManager; // Sistema de entrada

    [Header("Objetos Interactuables")]
    public GameObject boton;  // Objeto interactuable (el botón)
    private Animator animatorBoton;  // Animator del botón
    public GameObject objeto1;  // Primer objeto para activar animación
    public GameObject objeto2;  // Segundo objeto para activar animación
    private Animator animatorObjeto1;  // Animator del primer objeto
    private Animator animatorObjeto2;  // Animator del segundo objeto

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        inputManager = GetComponent<InputManager>();

        // Inicializamos los animadores
        if (boton != null)
        {
            animatorBoton = boton.GetComponent<Animator>();
        }
        if (objeto1 != null)
        {
            animatorObjeto1 = objeto1.GetComponent<Animator>();
        }
        if (objeto2 != null)
        {
            animatorObjeto2 = objeto2.GetComponent<Animator>();
        }
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
                objeto.Interactuar();  // Ejecutar la acción del objeto interactuado

                // Si el jugador interactúa con el botón
                if (hit.collider.gameObject == boton && animatorBoton != null)
                {
                    animatorBoton.SetTrigger("Move");  // Animación del botón
                    ActivarAnimacionesObjetos();
                }
            }
        }
    }

    /// <summary>
    /// Activa las animaciones de los otros dos objetos.
    /// </summary>
    void ActivarAnimacionesObjetos()
    {
        if (animatorObjeto1 != null)
        {
            animatorObjeto1.SetTrigger("Move");  // Animación del primer objeto
        }

        if (animatorObjeto2 != null)
        {
            animatorObjeto2.SetTrigger("Move");  // Animación del segundo objeto
        }
    }
}
