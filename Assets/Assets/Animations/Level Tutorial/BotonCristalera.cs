using UnityEngine;
using System.Collections;  // Necesario para las Coroutines

public class BotonCristalera : MonoBehaviour
{
    // Referencias públicas para asignar en el Inspector
    public Animator botonCristalera;
    public GameObject objetoADesactivar;
    public GameObject objetoBDesactivar;
    public GameObject objetoAActivar;
    private BoxCollider colliderE;

    // Controla si el jugador está cerca de la plataforma
    private bool isPlayerNear = false;
    private bool canInteract = true;  // Variable que controla si se puede interactuar
    private bool hasInteracted = false;  // Variable que asegura que la interacción solo se realice una vez
    private InputManager inputManager;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        colliderE = GetComponent<BoxCollider>();
    }

    void Update()
    {
        // Solo permitir la interacción si el jugador está cerca y presiona la tecla "E", 
        // si se puede interactuar, y si no se ha interactuado ya
        if (isPlayerNear && inputManager.IsInteractPressed && canInteract && !hasInteracted)
        {
            if (botonCristalera != null)
            {
                // Activar la animación
                botonCristalera.SetTrigger("Move");

                // Marcar como interactuado para que no se active de nuevo hasta que se haya completado el proceso
                hasInteracted = true;

                // Desactivar la posibilidad de interactuar durante 1 segundo
                StartCoroutine(DisableInteractionTemporarily());

                // Iniciar la corutina para esperar el final de la animación
                StartCoroutine(WaitForAnimationToEnd());
            }
        }
    }

    // Corutina para desactivar la interacción durante 1 segundo
    private IEnumerator DisableInteractionTemporarily()
    {
        canInteract = false;  // Desactivar la interacción
        yield return new WaitForSeconds(1f);  // Esperar 1 segundo
        canInteract = true;  // Reactivar la interacción
        hasInteracted = false;  // Permitir una nueva interacción
    }

    // Corutina para esperar el final de la animación y cambiar objetos
    private IEnumerator WaitForAnimationToEnd()
    {
        // Esperar a que termine la animación
        yield return new WaitForSeconds(2f);

        // Cambiar los objetos cuando la animación haya terminado
        if (objetoADesactivar != null)
        {
            objetoADesactivar.SetActive(false);
        }

        if (objetoADesactivar != null)
        {
            objetoBDesactivar.SetActive(false);
        }

        if (objetoAActivar != null)
        {
            objetoAActivar.SetActive(true);
        }

        gameObject.tag = "Untagged";
    }

    // Detectar cuando el jugador entra al área de la plataforma (trigger)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    // Detectar cuando el jugador sale del área de la plataforma (trigger)
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
