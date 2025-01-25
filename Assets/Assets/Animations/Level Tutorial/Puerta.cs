using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para cambiar de escena
using System.Collections;  // Para Coroutines
using UnityEngine.UI;  // Para controlar el Canvas y sus elementos

public class Puerta : MonoBehaviour
{
    // Referencias públicas para asignar en el Inspector
    public Animator puerta;
    public string escenaDestino;  // Nombre de la escena a la que se quiere teletransportar
    public Image canvasImage;  // Referencia a la imagen que se usará para el efecto de opacidad
    public float fadeDuration = 1f;  // Duración de la animación de opacidad en segundos (editable en el Inspector)

    private bool isPlayerNear = false;
    private bool canInteract = true;
    private bool hasInteracted = false;
    private InputManager inputManager;

    void Start()
    {
        inputManager = GetComponent<InputManager>();

        // Asegurarse de que la imagen del Canvas comience completamente transparente
        if (canvasImage != null)
        {
            Color color = canvasImage.color;
            color.a = 0f;  // Opacidad inicial en 0
            canvasImage.color = color;
        }
    }

    void Update()
    {
        // Solo permitir la interacción si el jugador está cerca y presiona la tecla "E"
        if (isPlayerNear && inputManager.IsInteractPressed && canInteract && !hasInteracted)
        {
            if (puerta != null)
            {
                // Activar la animación de la puerta
                puerta.SetTrigger("Move");

                // Marcar como interactuado para evitar activaciones repetidas
                hasInteracted = true;

                // Iniciar la transición del Canvas y el cambio de escena
                StartCoroutine(TransicionCanvasYEscena());
            }
        }
    }

    // Corutina para manejar la transición del Canvas y cambiar de escena
    private IEnumerator TransicionCanvasYEscena()
    {
        // Transición del Canvas (opacidad de 0 a 1)
        yield return StartCoroutine(FadeCanvas(0f, 1f, fadeDuration));

        // Esperar a que la animación de la puerta termine
        yield return new WaitForSeconds(1.5f);

        // Cambiar a la nueva escena
        SceneManager.LoadScene(escenaDestino);
    }

    // Corutina para manejar el fade del Canvas
    private IEnumerator FadeCanvas(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // Interpolar el valor de opacidad
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);

            // Actualizar el color de la imagen del Canvas
            if (canvasImage != null)
            {
                Color color = canvasImage.color;
                color.a = newAlpha;
                canvasImage.color = color;
            }

            yield return null;
        }

        // Asegurarse de que llegue al valor final
        if (canvasImage != null)
        {
            Color color = canvasImage.color;
            color.a = endAlpha;
            canvasImage.color = color;
        }
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
