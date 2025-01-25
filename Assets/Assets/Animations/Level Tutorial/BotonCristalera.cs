using UnityEngine;
using System.Collections;  // Necesario para las Coroutines

public class BotonCristalera : MonoBehaviour
{
    // Referencias p�blicas para asignar en el Inspector
    public Animator botonCristalera;
    public GameObject objetoADesactivar;
    public GameObject objetoBDesactivar;
    public GameObject objetoAActivar;
    private BoxCollider colliderE;

    // Controla si el jugador est� cerca de la plataforma
    private bool isPlayerNear = false;
    private bool canInteract = true;  // Variable que controla si se puede interactuar
    private bool hasInteracted = false;  // Variable que asegura que la interacci�n solo se realice una vez
    private InputManager inputManager;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
        colliderE = GetComponent<BoxCollider>();
    }

    void Update()
    {
        // Solo permitir la interacci�n si el jugador est� cerca y presiona la tecla "E", 
        // si se puede interactuar, y si no se ha interactuado ya
        if (isPlayerNear && inputManager.IsInteractPressed && canInteract && !hasInteracted)
        {
            if (botonCristalera != null)
            {
                // Activar la animaci�n
                botonCristalera.SetTrigger("Move");

                // Marcar como interactuado para que no se active de nuevo hasta que se haya completado el proceso
                hasInteracted = true;

                // Desactivar la posibilidad de interactuar durante 1 segundo
                StartCoroutine(DisableInteractionTemporarily());

                // Iniciar la corutina para esperar el final de la animaci�n
                StartCoroutine(WaitForAnimationToEnd());
            }
        }
    }

    // Corutina para desactivar la interacci�n durante 1 segundo
    private IEnumerator DisableInteractionTemporarily()
    {
        canInteract = false;  // Desactivar la interacci�n
        yield return new WaitForSeconds(1f);  // Esperar 1 segundo
        canInteract = true;  // Reactivar la interacci�n
        hasInteracted = false;  // Permitir una nueva interacci�n
    }

    // Corutina para esperar el final de la animaci�n y cambiar objetos
    private IEnumerator WaitForAnimationToEnd()
    {
        // Esperar a que termine la animaci�n
        yield return new WaitForSeconds(2f);

        // Cambiar los objetos cuando la animaci�n haya terminado
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

    // Detectar cuando el jugador entra al �rea de la plataforma (trigger)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    // Detectar cuando el jugador sale del �rea de la plataforma (trigger)
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
