using UnityEngine;

public class VisibilityTrigger : MonoBehaviour
{
    // Arrastra el GameObject desde el inspector
    public GameObject targetObject;

    private void Start()
    {
        // Asegúrate de que el cubo sea invisible al inicio
        GetComponent<Renderer>().enabled = false;

        // Si hay un objeto asignado, lo desactiva al principio
        if (targetObject != null)
        {
            targetObject.SetActive(false); // Esto funciona para cualquier tipo de objeto
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Cuando un objeto entra en contacto con el cubo, activa el objeto objetivo
        if (targetObject != null)
        {
            targetObject.SetActive(true); // Hacemos visible el objeto, incluyendo Canvas
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Cuando el objeto deja de estar en contacto, lo desactiva
        if (targetObject != null)
        {
            targetObject.SetActive(false); // Hacemos invisible de nuevo el objeto
        }
    }
}
