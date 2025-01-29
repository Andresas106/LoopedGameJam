using UnityEngine;

/// <summary>
/// Este script permite que el botón interactuable realice alguna acción cuando el jugador lo presiona.
/// </summary>
public class BotonInteraccion : MonoBehaviour, IInteractuable
{
    public void Interactuar()
    {
        // Este método es llamado por el jugador para activar la interacción.
        // Aquí puedes agregar la lógica adicional que deseas que ocurra al interactuar con el botón.
        Debug.Log("Botón presionado");
    }
}
