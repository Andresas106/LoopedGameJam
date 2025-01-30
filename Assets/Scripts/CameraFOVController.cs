using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFOVController : MonoBehaviour
{
    public Slider fovSlider;  // Referencia al slider, si está presente
    private Camera[] cameras; // Arreglo de cámaras con la etiqueta "Camara"
    private const float defaultFOV = 60f;  // FOV por defecto
    private const float minFOV = 45f;      // FOV mínimo
    private const float maxFOV = 80f;      // FOV máximo

    void Start()
    {
        // Buscar todas las cámaras con la etiqueta "Camara"
        GameObject[] cameraObjects = GameObject.FindGameObjectsWithTag("Camara");

        // Verificar si se encontraron cámaras
        if (cameraObjects.Length == 0)
        {
            Debug.LogError("No se encontraron cámaras con la etiqueta 'Camara' en la escena.");
            return;
        }

        // Obtener las cámaras de esos objetos
        cameras = new Camera[cameraObjects.Length];
        for (int i = 0; i < cameraObjects.Length; i++)
        {
            cameras[i] = cameraObjects[i].GetComponent<Camera>();
        }

        // Cargar el valor guardado o usar el predeterminado
        float savedFOV = PlayerPrefs.GetFloat("CameraFOV", defaultFOV);

        // Aplicar el FOV inicial
        UpdateAllCamerasFOV(savedFOV);

        // Configurar el slider si está presente
        if (fovSlider)
        {
            fovSlider.minValue = minFOV;
            fovSlider.maxValue = maxFOV;
            fovSlider.value = savedFOV;
            fovSlider.onValueChanged.AddListener(UpdateAllCamerasFOV);
        }
    }

    // Método para actualizar el FOV de todas las cámaras
    void UpdateAllCamerasFOV(float newFOV)
    {
        if (cameras == null || cameras.Length == 0) return;

        // Aplicar el nuevo FOV a todas las cámaras
        foreach (Camera cam in cameras)
        {
            cam.fieldOfView = newFOV;
        }
        // Guardar el valor del FOV en PlayerPrefs para que persista entre escenas
        PlayerPrefs.SetFloat("CameraFOV", newFOV);
    }

    // Método para aplicar el FOV cuando la escena se carga (sin importar si el slider está presente)
    public static void ApplySavedFOV()
    {
        // Cargar el valor guardado del FOV
        float savedFOV = PlayerPrefs.GetFloat("CameraFOV", 60f); // Valor por defecto: 60f

        // Buscar todas las cámaras con la etiqueta "Camara"
        GameObject[] cameraObjects = GameObject.FindGameObjectsWithTag("Camara");

        // Aplicar el FOV a todas las cámaras encontradas
        foreach (GameObject cameraObj in cameraObjects)
        {
            Camera cam = cameraObj.GetComponent<Camera>();
            if (cam != null)
            {
                cam.fieldOfView = savedFOV;
            }
        }
    }
}
