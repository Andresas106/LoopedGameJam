using UnityEngine;

public class TogglePopup : MonoBehaviour
{
    [SerializeField] private GameObject popup; // Asigna el GameObject del popup en el Inspector
    [SerializeField] private MonoBehaviour playerController; // Asigna el script de movimiento del jugador en el Inspector

    void Start()
    {
        popup.SetActive(false); // Oculta el popup al iniciar la escena
        Cursor.visible = false; // Oculta el cursor al inicio
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor al inicio
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isActive = !popup.activeSelf;
            popup.SetActive(isActive);
            Cursor.visible = isActive; // Hace visible el cursor cuando el popup está activo
            Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked; // Libera el cursor cuando el popup está activo
            
            if (playerController != null)
            {
                playerController.enabled = !isActive; // Desactiva el movimiento del jugador cuando el popup está activo
            }
        }
    }
}