using UnityEngine;

public class LuzParpadeo : MonoBehaviour
{
    public Light luz;  // Luz a parpadear
    public float minTiempoParpadeo = 0.1f;  // Tiempo mínimo entre parpadeos
    public float maxTiempoParpadeo = 1f;    // Tiempo máximo entre parpadeos
    private float tiempoSiguienteParpadeo;  // Tiempo para el siguiente parpadeo
    private bool luzEncendida = true;       // Estado actual de la luz

    void Start()
    {
        if (luz == null)
        {
            luz = GetComponent<Light>(); // Si no se asigna, busca una luz en el mismo GameObject
        }

        // Inicializa el primer parpadeo
        EstablecerTiempoParpadeo();
    }

    void Update()
    {
        // Resta el tiempo para el siguiente parpadeo
        tiempoSiguienteParpadeo -= Time.deltaTime;

        // Si ha pasado el tiempo para el siguiente parpadeo
        if (tiempoSiguienteParpadeo <= 0)
        {
            Parpadear();
            EstablecerTiempoParpadeo(); // Establecer un nuevo tiempo aleatorio para el siguiente parpadeo
        }
    }

    // Función para hacer parpadear la luz
    void Parpadear()
    {
        luzEncendida = !luzEncendida; // Cambia el estado de la luz (encender o apagar)
        luz.enabled = luzEncendida;
    }

    // Establece un nuevo tiempo aleatorio entre los parpadeos
    void EstablecerTiempoParpadeo()
    {
        tiempoSiguienteParpadeo = Random.Range(minTiempoParpadeo, maxTiempoParpadeo);
    }
}
