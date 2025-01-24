using UnityEngine;

public class PlatformInteraction : MonoBehaviour
{
    // Referencia pública para asignar el Animator manualmente en el Inspector
    public Animator platformAnimator;
    public Animator platformAnimator1;

    // Controla si el jugador está cerca de la plataforma
    private bool isPlayerNear = false;

    void Update()
    {
        // Solo permitir la interacción si el jugador está cerca y presiona la tecla "E"
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Asegúrate de que la referencia al Animator esté configurada correctamente
            if (platformAnimator != null)
            {
                    platformAnimator.SetTrigger("Move");
                    platformAnimator1.SetTrigger("Move");
            }
            else
            {
                Debug.LogError("Animator no asignado en el Inspector");
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
