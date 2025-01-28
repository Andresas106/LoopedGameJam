using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{
    // GameObject que ser� teletransportado. Selecci�nalo en el Inspector.
    public GameObject objectToTeleport;

    // Identifica si el trigger lo activ� el jugador.
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra es el Player (puedes cambiar esto seg�n tu configuraci�n).
        if (other.CompareTag("Player"))
        {
            // Verifica que el objeto a teletransportar est� asignado
            if (objectToTeleport != null)
            {
                // Teletransporta el objeto al centro del GameObject que tiene este script
                objectToTeleport.transform.position = transform.position;

                // (Opcional) Reinicia la rotaci�n del objeto teletransportado
                objectToTeleport.transform.rotation = Quaternion.identity;
            }
        }
    }
}
