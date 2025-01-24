using UnityEngine;

public class PlatformInteraction : MonoBehaviour
{
    private Animator animator;
    private bool isPlayerNear = false;  // Para saber si el jugador est� cerca

    void Start()
    {
        // Obtener el Animator del objeto
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Solo permitir la interacci�n si el jugador est� cerca y presiona la tecla "E"
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Alternar la animaci�n (mover hacia arriba o hacia abajo)
            animator.SetTrigger("ToggleMove");
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
