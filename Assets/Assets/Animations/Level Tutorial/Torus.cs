using UnityEngine;

public class TorusInteractivo : MonoBehaviour, IInteractuable
{
    public Animator objetoAnimado; // Objeto que se animar� al interactuar

    public void Interactuar()
    {
        // Activa la animaci�n del objeto asociado
        if (objetoAnimado != null)
        {
            objetoAnimado.SetTrigger("Move");
        }

        // Desactiva el torus
        gameObject.SetActive(false);

        Debug.Log("Torus interactuado: animaci�n activada y torus desactivado.");
    }
}
