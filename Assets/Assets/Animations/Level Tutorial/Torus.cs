using UnityEngine;

public class TorusInteractivo : MonoBehaviour, IInteractuable
{
    public Animator objetoAnimado; // Objeto que se animará al interactuar
    public PlayerController pc;
    public AudioClip plataforma;
    public AudioSource plataforma1;

    public void Interactuar()
    {
        // Activa la animación del objeto asociado
        if (objetoAnimado != null)
        {
            objetoAnimado.SetTrigger("Move");

            plataforma1.PlayOneShot(plataforma); // Reproducir el sonido del botón
      
        }
        pc.havePower = true;
        // Desactiva el torus
        gameObject.SetActive(false);

        Debug.Log("Torus interactuado: animación activada y torus desactivado.");
    }
}
