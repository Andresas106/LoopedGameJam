using UnityEngine;
using UnityEngine.UI;  // Necesario para trabajar con UI
using UnityEngine.SceneManagement;  // Para cargar nuevas escenas

public class ChangeSceneButton : MonoBehaviour
{
    // Este es el nombre de la escena que se quiere cargar
    public string sceneName;

    // Este método debe estar vinculado al botón en Unity
    public void OnButtonClick()
    {
        // Cambia a la escena especificada
        SceneManager.LoadScene(sceneName);
    }
}
