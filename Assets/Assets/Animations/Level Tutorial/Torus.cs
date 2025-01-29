using UnityEngine;

public class TorusInteractivo : MonoBehaviour, IInteractuable
{
    public Animator objetoAnimado; // Objeto que se animará al interactuar
    public PlayerController pc;

    public void Interactuar()
    {
        // Activa la animación del objeto asociado
        if (objetoAnimado != null)
        {
            objetoAnimado.SetTrigger("Move");
        }
        pc.havePower = true;
        // Desactiva el torus
        gameObject.SetActive(false);

        Debug.Log("Torus interactuado: animación activada y torus desactivado.");
    }
}
