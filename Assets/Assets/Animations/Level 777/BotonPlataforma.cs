using UnityEngine;
using System.Collections;  // Necesario para las Coroutines

public class PlatformInteraction : MonoBehaviour
{
    // Referencias públicas para asignar los Animators manualmente en el Inspector
    public Animator platformAnimator;
    public Animator platformAnimator1;

    // Controla si el jugador está cerca de la plataforma
    private bool isPlayerNear = false;
    private bool canInteract = true;  // Variable que controla si se puede interactuar

    void Update()
    {
        // Solo permitir la interacción si el jugador está cerca y presiona la tecla "E", y si se puede interactuar
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            // Asegúrate de que las referencias a los Animator estén configuradas correctamente
            if (platformAnimator != null && platformAnimator1 != null)
            {
                // Activar los triggers para la animación
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

    // Corutina para desactivar la interacción durante 5 segundos
    private IEnumerator DisableInteractionTemporarily()
    {
        canInteract = false;  // Desactivar la interacción
        yield return new WaitForSeconds(5.5f);  // Esperar 5 segundos
        canInteract = true;  // Reactivar la interacción
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
