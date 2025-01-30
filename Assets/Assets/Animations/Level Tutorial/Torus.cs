using UnityEngine;

public class TorusInteractivo : MonoBehaviour, IInteractuable
{
    public Animator objetoAnimado; // Objeto que se animar� al interactuar
    public PlayerController pc;
    public AudioClip plataforma;
    public AudioSource plataforma1;

    public void Interactuar()
    {
        // Activa la animaci�n del objeto asociado
        if (objetoAnimado != null)
        {
            objetoAnimado.SetTrigger("Move");

            plataforma1.PlayOneShot(plataforma); // Reproducir el sonido del bot�n
      
        }
        pc.havePower = true;
        // Desactiva el torus
        gameObject.SetActive(false);

        Debug.Log("Torus interactuado: animaci�n activada y torus desactivado.");
    }
}
