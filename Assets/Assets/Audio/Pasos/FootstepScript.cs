using System.Collections;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public AudioSource footstepAudio; // Componente de audio que reproduce los pasos
    public float pitchRange = 0.1f; // Margen para aleatorizar el pitch (± pitchRange)
    public float walkInterval = 0.5f; // Intervalo entre pasos al caminar
    public float sprintInterval = 0.3f; // Intervalo entre pasos al correr (Shift)
    public PlayerController pc;

   
    private Coroutine footstepCoroutine; // Referencia a la corrutina que reproduce pasos
    private InputManager inputManager;

    void Awake()
    {
        inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        bool shouldPlayFootsteps = pc.isMoving && !pc.isJumping;

        if (shouldPlayFootsteps && footstepCoroutine == null)
        {
            footstepCoroutine = StartCoroutine(PlayFootsteps());
        }
        else if (!shouldPlayFootsteps && footstepCoroutine != null)
        {
            StopCoroutine(footstepCoroutine);
            footstepCoroutine = null;
        }
    }

    IEnumerator PlayFootsteps()
    {
        while (true)
        {
            if (footstepAudio != null)
            {
                footstepAudio.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
                footstepAudio.Play();

                yield return new WaitForSeconds(footstepAudio.clip.length);

                float currentInterval = inputManager.IsRunPressed ? sprintInterval : walkInterval;
                yield return new WaitForSeconds(currentInterval);
            }
            else
            {
                Debug.LogWarning("No se ha asignado un AudioSource al script.");
                yield break;
            }

            // Verificar si debemos continuar reproduciendo pasos
            if (!pc.isMoving || pc.isJumping)
            {
                footstepCoroutine = null;
                yield break;
            }
        }
    }
}
