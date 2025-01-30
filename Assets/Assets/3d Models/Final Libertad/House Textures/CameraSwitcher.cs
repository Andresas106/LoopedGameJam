using System.Collections;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [Header("Cámaras a cambiar")]
    public Camera[] cameras;  // Array para las cámaras

    [Header("Duración de cada cámara")]
    public float[] durations;  // Duración en segundos de cada cámara

    private int currentCameraIndex = 0;

    void Start()
    {
        if (cameras.Length != durations.Length)
        {
            Debug.LogError("El número de cámaras y la cantidad de duraciones no coinciden.");
            return;
        }

        StartCoroutine(SwitchCameras());
    }

    IEnumerator SwitchCameras()
    {
        // Asegura que todas las cámaras están apagadas inicialmente
        foreach (Camera cam in cameras)
        {
            cam.gameObject.SetActive(false);
        }

        // Activa la primera cámara al inicio
        cameras[currentCameraIndex].gameObject.SetActive(true);

        // Ciclo a través de las cámaras con sus respectivas duraciones
        while (true)
        {
            // Espera por la duración especificada
            yield return new WaitForSeconds(durations[currentCameraIndex]);

            // Desactiva la cámara actual
            cameras[currentCameraIndex].gameObject.SetActive(false);

            // Avanza al siguiente índice de cámara (si llegamos al final, volvemos al inicio)
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

            // Activa la siguiente cámara
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }
    }
}
