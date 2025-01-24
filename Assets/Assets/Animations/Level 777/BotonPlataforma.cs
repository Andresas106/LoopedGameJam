using UnityEngine;

public class PlatformInteraction : MonoBehaviour
{
    // Referencia p�blica para asignar el Animator manualmente en el Inspector
    public Animator platformAnimator;
    public Animator platformAnimator1;

    // Controla si el jugador est� cerca de la plataforma
    private bool isPlayerNear = false;

    void Update()
    {
        // Solo permitir la interacci�n si el jugador est� cerca y presiona la tecla "E"
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Aseg�rate de que la referencia al Animator est� configurada correctamente
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
