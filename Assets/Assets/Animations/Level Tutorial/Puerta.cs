using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Controla la animación y el cambio de escena al interactuar con la puerta.
/// </summary>
public class PuertaInteractiva : MonoBehaviour, IInteractuable
{
    public Animator puerta;
    public string escenaDestino;
    public Image canvasImage;
    public float fadeDuration = 1f;
    public GameObject player;
    public AudioClip clickSound; // Para asignar el sonido desde el Inspector
    private AudioSource audioSource; // Para controlar el audio

    private bool hasInteracted = false;

    public void Interactuar()
    {
        if (!hasInteracted)
        {
            Debug.Log("Hola");
            audioSource = GetComponent<AudioSource>();
            audioSource = gameObject.AddComponent<AudioSource>();
            hasInteracted = true;
            puerta.SetTrigger("Move");
            audioSource.PlayOneShot(clickSound); // Reproducir el sonido del botón
            player.GetComponent<PlayerController>().enabled = false;
            StartCoroutine(TransicionCanvasYEscena());
        }
    }

    private IEnumerator TransicionCanvasYEscena()
    {
        yield return StartCoroutine(FadeCanvas(0f, 1f, fadeDuration));
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(escenaDestino);
    }

    private IEnumerator FadeCanvas(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            if (canvasImage != null)
            {
                Color color = canvasImage.color;
                color.a = newAlpha;
                canvasImage.color = color;
            }
            yield return null;
        }
    }
}
