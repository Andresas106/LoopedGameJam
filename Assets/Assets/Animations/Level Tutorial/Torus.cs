using UnityEngine;

public class TorusInteractivo : MonoBehaviour, IInteractuable
{
    public Animator objetoAnimado; // Objeto que se animar� al interactuar
    private InputManager inputManager; // Referencia al InputManager

    void Start()
    {
        // Obtener el InputManager desde el objeto (si est� en el mismo objeto)
        inputManager = FindObjectOfType<InputManager>();

        // Desactivar la habilidad de "morir" al inicio
        if (inputManager != null)
        {
            inputManager.SetDiePressed(false); // Asegura que la acci�n de "morir" est� desactivada al principio
        }
    }

    public void Interactuar()
    {
        // Activa la animaci�n del objeto asociado
        if (objetoAnimado != null)
        {
            objetoAnimado.SetTrigger("Move");
        }

        // Desactiva el torus
        gameObject.SetActive(false);

        // Activar la habilidad de "morir" en el InputManager
        if (inputManager != null)
        {
            inputManager.SetDiePressed(true); // Activa la acci�n de morir
        }

        Debug.Log("Torus interactuado: animaci�n activada y torus desactivado.");
    }
}
