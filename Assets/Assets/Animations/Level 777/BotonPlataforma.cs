using UnityEngine;

public class PlatformInteraction : MonoBehaviour
{
    private Animator animator;
    private bool isPlayerNear = false;  // Para saber si el jugador está cerca

    void Start()
    {
        // Obtener el Animator del objeto
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Solo permitir la interacción si el jugador está cerca y presiona la tecla "E"
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            // Alternar la animación (mover hacia arriba o hacia abajo)
            animator.SetTrigger("ToggleMove");
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
