using UnityEngine;
using System.Collections;  // Necesario para las Coroutines

public class Cachibache : MonoBehaviour
{
    // Referencias públicas para asignar los Animators manualmente en el Inspector
    public Animator puente;
    public GameObject cachibache;

    // Controla si el jugador está cerca de la plataforma
    private bool isPlayerNear = false;
    private bool canInteract = true;  // Variable que controla si se puede interactuar
    private bool hasInteracted = false;  // Variable que asegura que la interacción solo se realice una vez
    private InputManager inputManager;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        // Solo permitir la interacción si el jugador está cerca y presiona la tecla "E", 
        // si se puede interactuar, y si no se ha interactuado ya
        if (isPlayerNear && inputManager.IsInteractPressed && canInteract && !hasInteracted)
        {
            // Asegúrate de que las referencias a los Animator estén configuradas correctamente
            if (puente != null)
            {
                // Activar los triggers para la animación
                puente.SetTrigger("Move");

                cachibache.SetActive(false);


                // Marcar como interactuado para que no se active de nuevo hasta que se haya completado el proceso
                hasInteracted = true;
            }
        }
    }

    // Detectar cuando el jugador entra al área de la plataforma (trigger)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Asegúrate de que el jugador tenga la etiqueta "Player"
        {
            isPlayerNear = true;  // El jugador está cerca de la plataforma
        }
    }

    // Detectar cuando el jugador sale del área de la plataforma (trigger)
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;  // El jugador ya no está cerca de la plataforma
        }
    }
}
