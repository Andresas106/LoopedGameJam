using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopupButtons : MonoBehaviour
{
    [SerializeField] private GameObject popup; // Asigna el GameObject del popup en el Inspector
    [SerializeField] private MonoBehaviour playerController; // Asigna el script de movimiento del jugador en el Inspector
    [SerializeField] private Button resumeButton; // Botón para reanudar el juego
    [SerializeField] private Button changeSceneButton; // Botón para cambiar de escena
    [SerializeField] private string sceneToLoad; // Nombre de la escena a cargar

    void OnEnable()
    {
        resumeButton.onClick.AddListener(ClosePopup);
        changeSceneButton.onClick.AddListener(ChangeScene);
    }

    void OnDisable()
    {
        resumeButton.onClick.RemoveListener(ClosePopup);
        changeSceneButton.onClick.RemoveListener(ChangeScene);
    }

    void ClosePopup()
    {
        popup.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f; // Reanudar el tiempo
        
        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }

    void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            Time.timeScale = 1f; // Asegurar que el tiempo no esté en pausa al cambiar de escena
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
