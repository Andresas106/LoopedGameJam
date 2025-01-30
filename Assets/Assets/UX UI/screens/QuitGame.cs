using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Método que se ejecutará al hacer clic en el botón
    public void ExitGame()
    {
        // Cierra el juego (solo funciona en una aplicación compilada)
        Application.Quit();

        // Si estás en el editor de Unity, esto detiene la ejecución
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
