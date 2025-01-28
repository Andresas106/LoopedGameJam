using UnityEngine;

public class TeleportOnTrigger : MonoBehaviour
{
    // GameObject que será teletransportado. Selecciónalo en el Inspector.
    public GameObject objectToTeleport;

    // Identifica si el trigger lo activó el jugador.
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra es el Player (puedes cambiar esto según tu configuración).
        if (other.CompareTag("Player"))
        {
            // Verifica que el objeto a teletransportar esté asignado
            if (objectToTeleport != null)
            {
                // Teletransporta el objeto al centro del GameObject que tiene este script
                objectToTeleport.transform.position = transform.position;

                // (Opcional) Reinicia la rotación del objeto teletransportado
                objectToTeleport.transform.rotation = Quaternion.identity;
            }
        }
    }
}
