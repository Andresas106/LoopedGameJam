using UnityEngine;
using UnityEngine.UI;  // Necesario para acceder al componente Button y Image
using UnityEngine.EventSystems;  // Necesario para manejar eventos de UI

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Referencia al componente Image del botón
    public Image buttonImage;

    // Este método se llama cuando el puntero entra en el área del botón
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            buttonImage.enabled = true; // Mostrar la imagen
        }
    }

    // Este método se llama cuando el puntero sale del área del botón
    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null)
        {
            buttonImage.enabled = false; // Ocultar la imagen
        }
    }
}
