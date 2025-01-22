using UnityEngine;
using System.Collections;

public class ShowHideElement : MonoBehaviour
{
    public GameObject elementToToggle;  // El objeto que queremos mostrar u ocultar
    public float delayTime = 3f;  // Tiempo en segundos antes de mostrar/ocultar el objeto
    public bool startVisible = false;  // Determina si el objeto es visible o no al inicio

    private void Start()
    {
        // Inicializa el estado del objeto (si debe estar visible o no al iniciar)
        elementToToggle.SetActive(startVisible);

        // Comienza la coroutine para el show/hide después del retraso
        StartCoroutine(ToggleVisibilityAfterDelay());
    }

    private IEnumerator ToggleVisibilityAfterDelay()
    {
        // Espera el tiempo especificado antes de cambiar la visibilidad
        yield return new WaitForSeconds(delayTime);

        // Cambia el estado del objeto (si está activo se desactiva, si está inactivo se activa)
        elementToToggle.SetActive(!elementToToggle.activeSelf);
    }
}
