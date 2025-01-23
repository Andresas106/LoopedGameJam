using UnityEngine;

public class RespawnOnKeyPress : MonoBehaviour
{
    // Objeto que se activará
    public GameObject objectToRespawn;

    // Variable para comprobar si estamos en contacto con el cubo
    private bool isPlayerInContact = false;

    private void OnTriggerEnter(Collider other)
    {
        // Detectamos si el jugador entra en contacto con el cubo
        if (other.CompareTag("Player"))
        {
            isPlayerInContact = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Detectamos si el jugador sale del cubo
        if (other.CompareTag("Player"))
        {
            isPlayerInContact = false;
        }
    }

    private void Update()
    {
        // Si el jugador está en contacto y presiona la tecla "E"
        if (isPlayerInContact && Input.GetKeyDown(KeyCode.E))
        {
            // Verifica que el objeto esté asignado
            if (objectToRespawn != null)
            {
                // Activar el objeto en la escena
                objectToRespawn.SetActive(true);

                // Comprobar si el objeto tiene un Animator y reproducir la animación
                Animator animator = objectToRespawn.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.Play("AnimationName"); // Cambia "AnimationName" por el nombre de la animación
                }
            }
        }
    }
}
