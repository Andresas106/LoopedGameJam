using UnityEngine;

/// <summary>
/// Este script permite que el bot�n interactuable realice alguna acci�n cuando el jugador lo presiona.
/// </summary>
public class BotonInteraccion : MonoBehaviour, IInteractuable
{
    public void Interactuar()
    {
        // Aqu� puedes agregar la l�gica que deseas que ocurra al interactuar con el bot�n.
        // En este caso, el bot�n solo activa la animaci�n de interacci�n, que ya se maneja desde el script de Interacci�nPlayer.
        Debug.Log("Bot�n presionado");
    }
}
