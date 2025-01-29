using UnityEngine;

/// <summary>
/// Este script permite que el bot�n interactuable realice alguna acci�n cuando el jugador lo presiona.
/// </summary>
public class BotonInteraccion : MonoBehaviour, IInteractuable
{
    public void Interactuar()
    {
        // Este m�todo es llamado por el jugador para activar la interacci�n.
        // Aqu� puedes agregar la l�gica adicional que deseas que ocurra al interactuar con el bot�n.
        Debug.Log("Bot�n presionado");
    }
}
