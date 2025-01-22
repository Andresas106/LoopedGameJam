using UnityEngine;
using System.Collections;

public class AudioPlayerWithClipRange : MonoBehaviour
{
    public AudioClip audioClip;        // El AudioClip (mp3) que se va a reproducir
    public float startTime = 0f;       // Tiempo en segundos donde debe comenzar el audio
    public float endTime = 0f;         // Tiempo en segundos donde debe terminar el audio
    public float delayTime = 0f;       // Retraso antes de que empiece a reproducirse
    public float additionalWaitTime = 0f; // Tiempo adicional de espera antes de iniciar la reproducción
    public bool loopAudio = false;     // Opción para poner el audio en bucle
    public bool useTimeRange = true;   // Opción para usar el Start Time y End Time (si está desmarcado, no se usa)

    private AudioSource audioSource;   // Componente AudioSource que se usará para reproducir el AudioClip

    private void Start()
    {
        // Crea un AudioSource y asigna el AudioClip
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip; // Asigna el AudioClip al AudioSource

        // Configura la opción de bucle en el AudioSource
        audioSource.loop = loopAudio;

        // Inicia la reproducción del audio después del retraso
        StartCoroutine(PlayAudioInRangeWithDelay());
    }

    private IEnumerator PlayAudioInRangeWithDelay()
    {
        // Espera el tiempo de retraso antes de comenzar
        yield return new WaitForSeconds(delayTime);

        // Espera el tiempo adicional antes de iniciar la reproducción
        yield return new WaitForSeconds(additionalWaitTime);

        // Si se desea usar el rango de tiempo, establece el tiempo de inicio
        if (useTimeRange)
        {
            audioSource.time = startTime; // Establece el tiempo de inicio del audio
        }

        // Reproduce el audio
        audioSource.Play();

        // Si no está en bucle, espera hasta que llegue al `endTime` y luego detiene el audio
        if (useTimeRange && !loopAudio)
        {
            yield return new WaitForSeconds(endTime - startTime);
            audioSource.Stop();
        }
    }
}
