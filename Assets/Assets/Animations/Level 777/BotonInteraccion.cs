using UnityEngine;

public class BotonInteraccion : MonoBehaviour, IInteractuable
{
    public AudioClip clickSound; // Para asignar el sonido desde el Inspector
    private AudioSource audioSource; // Para controlar el audio

    private void Start()
    {
        // Inicializar el componente AudioSource si no está presente
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false; // Asegurarse de que el audio no se reproduzca automáticamente al inicio
    }

    public void Interactuar()
    {
        // Este método es llamado por el jugador para activar la interacción
        if (clickSound != null && audioSource != null)
        {
            if (!audioSource.isPlaying) // Comprobar si ya está reproduciendo un audio
            {
                audioSource.PlayOneShot(clickSound); // Reproducir el sonido del botón
            }
        }

        Debug.Log("Botón presionado");
    }
}
