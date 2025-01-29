using UnityEngine;

/// <summary>
/// Este script permite que el botón interactuable realice alguna acción cuando el jugador lo presiona.
/// </summary>
public class BotonInteraccion : MonoBehaviour, IInteractuable
{
    public void Interactuar()
    {
        // Aquí puedes agregar la lógica que deseas que ocurra al interactuar con el botón.
        // En este caso, el botón solo activa la animación de interacción, que ya se maneja desde el script de InteracciónPlayer.
        Debug.Log("Botón presionado");
    }
}
