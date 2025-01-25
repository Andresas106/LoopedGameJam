using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para cambiar de escena
using System.Collections;  // Para Coroutines

public class Puerta : MonoBehaviour
{
    // Referencias públicas para asignar en el Inspector
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
        // Solo permitir la interacción si el jugador está cerca y presiona la tecla "E", 
        if (isPlayerNear && inputManager.IsInteractPressed && canInteract && !hasInteracted)
        {
            if (puerta != null)
            {
                //Activar la animación de la puerta
                puerta.SetTrigger("Move");

                // Marcar como interactuado para evitar activaciones repetidas
                hasInteracted = true;

                // Iniciar corutina para esperar a que la animación termine antes de cambiar de escena
                StartCoroutine(EsperarTiempo());
            }
        }
    }

    // Corutina para esperar a que termine la animación antes de cambiar de escena
    private IEnumerator EsperarTiempo()
    {

        // Esperar a que la animación termine
        yield return new WaitForSeconds(1.5f);

        // Cambiar a la nueva escena
        SceneManager.LoadScene(escenaDestino);
    }

    // Detectar cuando el jugador entra al área de la puerta (trigger)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    // Detectar cuando el jugador sale del área de la puerta (trigger)
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
