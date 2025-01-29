using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimacionConCamara : MonoBehaviour
{
    [Header("Referencias")]
    public Camera playerCamera;        // Cámara del jugador
    public MonoBehaviour playerScript; // Script del jugador (como el de movimiento o interacción)
    public Animator cameraAnimator;    // Animator para controlar animaciones de la cámara
    public Canvas canvasPantallaNegra; // Canvas que contiene la imagen negra
    public Image pantallaNegra;        // Imagen negra en el Canvas para simular el parpadeo
    public FootstepScript footsteps;

    [Header("Configuración")]
    public float retrasoInicio = 10f;       // Tiempo de espera antes de iniciar el efecto
    public float duracionPrimerParpadeo = 2f;   // Duración del primer parpadeo (más lento)
    public float duracionParpadeoRapido = 0.3f; // Duración de los parpadeos rápidos
    public float tiempoEntreParpadeos = 0.15f;  // Tiempo entre parpadeos rápidos
    public float animacionDuracion = 2f;    // Duración de la animación de la cámara

    private bool isAnimationPlaying = false;

    void Start()
    {
        // Asegurar que el Canvas esté activado y completamente negro al inicio
        if (canvasPantallaNegra != null)
        {
            canvasPantallaNegra.gameObject.SetActive(false);
        }
        if (pantallaNegra != null)
        {
            Color color = pantallaNegra.color;
            color.a = 1f;
            pantallaNegra.color = color;
        }

        // Espera antes de iniciar el efecto de parpadeo
        StartCoroutine(EsperarYComenzar());
    }

    private IEnumerator EsperarYComenzar()
    {
        yield return new WaitForSeconds(retrasoInicio);

        // Iniciar el efecto de abrir los ojos con parpadeos suaves
        StartCoroutine(EfectoAbrirOjos());
    }

    // Corutina que maneja el efecto de abrir los ojos con parpadeos suaves
    private IEnumerator EfectoAbrirOjos()
    {
        if (pantallaNegra != null)
        {
            Color color = pantallaNegra.color;
            canvasPantallaNegra.gameObject.SetActive(true);
            // 🔹 PRIMER PESTAÑEO LENTO (de negro a transparente)
            yield return StartCoroutine(FadePantallaNegra(1f, 0f, duracionPrimerParpadeo));

            // 🔹 SEGUNDO PESTAÑEO RÁPIDO
            yield return StartCoroutine(FadePantallaNegra(0f, 1f, duracionParpadeoRapido / 2)); // Cierra rápido
            yield return StartCoroutine(FadePantallaNegra(1f, 0f, duracionParpadeoRapido / 2)); // Abre rápido

            // 🔹 Pequeño delay antes de la animación final
            yield return new WaitForSeconds(tiempoEntreParpadeos);

            // 🔹 Última transición de negro a transparente
            yield return StartCoroutine(FadePantallaNegra(1f, 0f, duracionParpadeoRapido));

            // Asegurar que desaparezca completamente y desactivar el Canvas
            color.a = 0f;
            pantallaNegra.color = color;
        }

        // Después del efecto de parpadeo, iniciar la animación con la cámara
        StartCoroutine(EjecutarAnimacionConCamara());
    }

    // Corutina para hacer un fundido de pantalla negra
    private IEnumerator FadePantallaNegra(float inicio, float fin, float duracion)
    {
        float elapsedTime = 0f;
        Color color = pantallaNegra.color;

        while (elapsedTime < duracion)
        {
            color.a = Mathf.Lerp(inicio, fin, elapsedTime / duracion);
            pantallaNegra.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegurar el valor final exacto
        color.a = fin;
        pantallaNegra.color = color;
    }

    // Corutina para ejecutar la animación de la cámara
    private IEnumerator EjecutarAnimacionConCamara()
    {
        isAnimationPlaying = true;

        // Ejecutar la animación de la cámara
        if (cameraAnimator != null)
        {
            cameraAnimator.SetTrigger("Move"); // Asegúrate de que en el Animator haya un Trigger llamado "Move"
        }

        // Esperar el tiempo de duración de la animación
        yield return new WaitForSeconds(animacionDuracion);

            playerScript.enabled = true;
            footsteps.enabled = true;
            cameraAnimator.enabled = false;
            this.enabled = false;
    }
}
