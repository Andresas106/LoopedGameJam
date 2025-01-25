using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para cambiar de escena
using System.Collections;  // Para Coroutines

public class Puerta : MonoBehaviour
{
    // Referencias p�blicas para asignar en el Inspector
    public Animator puerta;
    public string escenaDestino;  // Nombre de la escena a la que se quiere teletransportar

    private bool isPlayerNear = false;
    private bool canInteract = true;
    private bool hasInteracted = false;
    private InputManager inputManager;

    void Start()
    {
        inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        // Solo permitir la interacci�n si el jugador est� cerca y presiona la tecla "E", 
        if (isPlayerNear && inputManager.IsInteractPressed && canInteract && !hasInteracted)
        {
            if (puerta != null)
            {
                //Activar la animaci�n de la puerta
                puerta.SetTrigger("Move");

                // Marcar como interactuado para evitar activaciones repetidas
                hasInteracted = true;

                // Iniciar corutina para esperar a que la animaci�n termine antes de cambiar de escena
                StartCoroutine(EsperarTiempo());
            }
        }
    }

    // Corutina para esperar a que termine la animaci�n antes de cambiar de escena
    private IEnumerator EsperarTiempo()
    {

        // Esperar a que la animaci�n termine
        yield return new WaitForSeconds(1.5f);

        // Cambiar a la nueva escena
        SceneManager.LoadScene(escenaDestino);
    }

    // Detectar cuando el jugador entra al �rea de la puerta (trigger)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    // Detectar cuando el jugador sale del �rea de la puerta (trigger)
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
