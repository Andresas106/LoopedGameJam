using UnityEngine;
using System.Collections;  // Necesario para las Coroutines

public class PlatformInteraction : MonoBehaviour
{
    // Referencias p�blicas para asignar los Animators manualmente en el Inspector
    public Animator platformAnimator;
    public Animator platformAnimator1;

    // Controla si el jugador est� cerca de la plataforma
    private bool isPlayerNear = false;
    private bool canInteract = true;  // Variable que controla si se puede interactuar

    void Update()
    {
        // Solo permitir la interacci�n si el jugador est� cerca y presiona la tecla "E", y si se puede interactuar
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            // Aseg�rate de que las referencias a los Animator est�n configuradas correctamente
            if (platformAnimator != null && platformAnimator1 != null)
            {
                // Activar los triggers para la animaci�n
                platformAnimator.SetTrigger("Move");
                platformAnimator1.SetTrigger("Move");

                // Desactivar la posibilidad de interactuar durante 5 segundos
                StartCoroutine(DisableInteractionTemporarily());
            }
            else
            {
                Debug.LogError("Animator no asignado en el Inspector");
            }
        }

    }

    // Corutina para desactivar la interacci�n durante 5 segundos
    private IEnumerator DisableInteractionTemporarily()
    {
        canInteract = false;  // Desactivar la interacci�n
        yield return new WaitForSeconds(5.5f);  // Esperar 5 segundos
        canInteract = true;  // Reactivar la interacci�n
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
