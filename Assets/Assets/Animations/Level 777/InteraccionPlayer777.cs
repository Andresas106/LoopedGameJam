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
    public GameObject boton1;  // Primer bot�n
    private Animator animatorBoton1;  // Animator del primer bot�n
    public GameObject boton2;  // Segundo bot�n
    private Animator animatorBoton2;  // Animator del segundo bot�n
    public GameObject boton3;  // Segundo bot�n
    private Animator animatorBoton3;  // Animator del segundo bot�n

    public GameObject objeto1;  // Primer objeto para activar animaci�n
    private Animator animatorObjeto1;  // Animator del primer objeto
    public GameObject objeto2;  // Segundo objeto para activar animaci�n
    private Animator animatorObjeto2;  // Animator del segundo objeto
    public GameObject objeto3;
    private Animator animatorObjeto3;
    public GameObject objeto4;
    private Animator animatorObjeto4;

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        inputManager = GetComponent<InputManager>();

        // Inicializamos los animadores de los botones y objetos
        if (boton1 != null)
        {
            animatorBoton1 = boton1.GetComponent<Animator>();
        }
        if (boton2 != null)
        {
            animatorBoton2 = boton2.GetComponent<Animator>();
        }
        if (boton3 != null)
        {
            animatorBoton3 = boton3.GetComponent<Animator>();
        }
        if (objeto1 != null)
        {
            animatorObjeto1 = objeto1.GetComponent<Animator>();
        }
        if (objeto2 != null)
        {
            animatorObjeto2 = objeto2.GetComponent<Animator>();
        }
        if (objeto3 != null)
        {
            animatorObjeto3 = objeto3.GetComponent<Animator>();
        }
        if (objeto4 != null)
        {
            animatorObjeto4 = objeto4.GetComponent<Animator>();
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

                // Si el jugador interact�a con el primer bot�n
                if (hit.collider.gameObject == boton1 && animatorBoton1 != null)
                {
                    animatorBoton1.SetTrigger("Move");  // Animaci�n del primer bot�n
                    ActivarAnimacionesBoton1();
                }

                // Si el jugador interact�a con el segundo bot�n
                if (hit.collider.gameObject == boton2 && animatorBoton2 != null)
                {
                    animatorBoton2.SetTrigger("Move");  // Animaci�n del segundo bot�n
                    ActivarAnimacionesBoton2();
                }
                // Si el jugador interact�a con el tercer bot�n
                if (hit.collider.gameObject == boton3 && animatorBoton3 != null)
                {
                    animatorBoton3.SetTrigger("Move");  // Animaci�n del segundo bot�n
                    ActivarAnimacionesBoton3();
                }
            }
        }
    }

    /// <summary>
    /// Activa las animaciones de los objetos asociados al primer bot�n.
    /// </summary>
    void ActivarAnimacionesBoton1()
    {
        if (animatorObjeto1 != null)
        {
            animatorObjeto1.SetTrigger("Move");  // Animaci�n del primer objeto
            animatorObjeto2.SetTrigger("Move");
        }
    }

    /// <summary>
    /// Activa las animaciones de los objetos asociados al segundo bot�n.
    /// </summary>
    void ActivarAnimacionesBoton2()
    {
        if (animatorObjeto2 != null)
        {
            animatorObjeto3.SetTrigger("Move");  // Animaci�n del segundo objeto
        }
    }
    void ActivarAnimacionesBoton3()
    {
        if (animatorObjeto4 != null)
        {
            animatorObjeto4.SetTrigger("Move");  // Animaci�n del segundo objeto
        }
    }
}
