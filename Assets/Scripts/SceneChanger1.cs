using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string nextSceneName = "NombreDeLaEscena"; // Nombre de la escena a la que quieres cambiar

    void Update()
    {
        // Detecta si se presiona la tecla 'P'
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Cambia a la escena directamente usando su nombre
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
