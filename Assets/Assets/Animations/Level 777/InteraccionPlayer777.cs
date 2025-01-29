using UnityEngine;

/// <summary>
/// Detecta qu� objeto est� mirando el jugador e interact�a con �l al presionar "E".
/// </summary>
public class InteraccionPlayer777 : MonoBehaviour
{
    [Header("Configuraci�n")]
    public Camera playerCamera;        // C�mara del jugador
    public float raycastDistance = 5f; // Distancia m�xima del raycast

    private InputManager inputManager; // Sistema de entrada

    [Header("Objetos Interactuables")]
    public GameObject boton;  // Objeto interactuable (el bot�n)
    private Animator animatorBoton;  // Animator del bot�n
    public GameObject objeto1;  // Primer objeto para activar animaci�n
    public GameObject objeto2;  // Segundo objeto para activar animaci�n
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
                objeto.Interactuar();  // Ejecutar la acci�n del objeto interactuado

                // Si el jugador interact�a con el bot�n
                if (hit.collider.gameObject == boton && animatorBoton != null)
                {
                    animatorBoton.SetTrigger("Move");  // Animaci�n del bot�n
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
            animatorObjeto1.SetTrigger("Move");  // Animaci�n del primer objeto
        }

        if (animatorObjeto2 != null)
        {
            animatorObjeto2.SetTrigger("Move");  // Animaci�n del segundo objeto
        }
    }
}
