using UnityEngine;
using System.Collections;  // Necesario para las Coroutines

public class Puente : MonoBehaviour
{
    // Referencias p�blicas para asignar los Animators manualmente en el Inspector
    public Animator puente;
    public Animator boton;

    // Controla si el jugador est� cerca de la plataforma
    private bool isPlayerNear = false;
    private bool canInteract = true;  // Variable que controla si se puede interactuar
    private bool hasInteracted = false;  // Variable que asegura que la interacci�n solo se realice una vez
    private InputManager inputManager;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        // Solo permitir la interacci�n si el jugador est� cerca y presiona la tecla "E", 
        // si se puede interactuar, y si no se ha interactuado ya
        if (isPlayerNear && inputManager.IsInteractPressed && canInteract && !hasInteracted)
        {
            // Aseg�rate de que las referencias a los Animator est�n configuradas correctamente
            if (puente != null)
            {
                // Activar los triggers para la animaci�n
                puente.SetTrigger("Move");

                if (boton != null)
                {
                    boton.SetTrigger("Move");
                }

                // Marcar como interactuado para que no se active de nuevo hasta que se haya completado el proceso
                hasInteracted = true;

                // Desactivar la posibilidad de interactuar durante 1 segundo
                StartCoroutine(DisableInteractionTemporarily());
            }
        }
    }

    // Corutina para desactivar la interacci�n durante 1 segundo
    private IEnumerator DisableInteractionTemporarily()
    {
        canInteract = false;  // Desactivar la interacci�n
        yield return new WaitForSeconds(1f);  // Esperar 1 segundo
        canInteract = true;  // Reactivar la interacci�n

        // Restablecer la posibilidad de interactuar despu�s de la espera
        hasInteracted = false;  // Permitir una nueva interacci�n
        puente.ResetTrigger("Move");
    }

    // Detectar cuando el jugador entra al �rea de la plataforma (trigger)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Aseg�rate de que el jugador tenga la etiqueta "Player"
        {
            isPlayerNear = true;  // El jugador est� cerca de la plataforma
        }
    }

    // Detectar cuando el jugador sale del �rea de la plataforma (trigger)
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;  // El jugador ya no est� cerca de la plataforma
        }
    }
}
