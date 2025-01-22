using UnityEngine;
using UnityEngine.EventSystems;  // Necesario para detectar eventos de UI

public class HoverShowElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // El objeto que se mostrará/ocultará al hacer hover
    public GameObject elementToToggle;

    // Se asegura de que el objeto esté oculto al inicio
    private void Start()
    {
        if (elementToToggle != null)
        {
            elementToToggle.SetActive(false);  // Asegura que el objeto esté oculto al inicio
        }
    }

    // Este método se llama cuando el puntero entra en el área del objeto
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (elementToToggle != null)
        {
            elementToToggle.SetActive(true);  // Muestra el objeto
        }
    }

    // Este método se llama cuando el puntero sale del área del objeto
    public void OnPointerExit(PointerEventData eventData)
    {
        if (elementToToggle != null)
        {
            elementToToggle.SetActive(false);  // Oculta el objeto
        }
    }
}
