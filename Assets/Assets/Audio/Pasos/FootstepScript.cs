using System.Collections;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public AudioSource footstepAudio; // Componente de audio que reproduce los pasos
    public float pitchRange = 0.1f; // Margen para aleatorizar el pitch (± pitchRange)
    public float walkInterval = 0.5f; // Intervalo entre pasos al caminar
    public float sprintInterval = 0.3f; // Intervalo entre pasos al correr (Shift)

    private bool isMoving = false; // Indica si el jugador está moviéndose
    private Coroutine footstepCoroutine; // Referencia a la corrutina que reproduce pasos

    void Update()
    {
        // Comprobar si el jugador está moviéndose (W, A, S, D)
        isMoving = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d");

        // Si está moviéndose y la corrutina no está activa, iniciar los pasos
        if (isMoving && footstepCoroutine == null)
        {
            footstepCoroutine = StartCoroutine(PlayFootsteps());
        }
        // Si no está moviéndose, detener los pasos
        else if (!isMoving && footstepCoroutine != null)
        {
            StopCoroutine(footstepCoroutine);
            footstepCoroutine = null;
        }
    }

    IEnumerator PlayFootsteps()
    {
        while (isMoving)
        {
            if (footstepAudio != null)
            {
                // Randomizar el pitch
                footstepAudio.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);

                // Reproducir el sonido completo
                footstepAudio.Play();

                // Esperar a que el sonido termine
                yield return new WaitForSeconds(footstepAudio.clip.length);

                // Determinar el intervalo dependiendo de si el jugador está sprintando
                float currentInterval = Input.GetKey(KeyCode.LeftShift) ? sprintInterval : walkInterval;

                // Esperar el intervalo configurado antes del siguiente paso
                yield return new WaitForSeconds(currentInterval);
            }
            else
            {
                Debug.LogWarning("No se ha asignado un AudioSource al script.");
                yield break;
            }
        }
    }
}
