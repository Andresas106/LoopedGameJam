using System.Collections;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    // Referencia al GameObject que quieres ocultar o mostrar
    public GameObject objetoOculto;

    // Tiempo en segundos que debe pasar antes de que el objeto se muestre
    public float tiempoDeEspera = 9f;

    // Start es llamado antes del primer frame
    void Start()
    {
        // Asegúrate de que el objeto esté oculto al inicio
        objetoOculto.SetActive(false);

        // Llamar a la función de mostrar después de un tiempo
        StartCoroutine(MostrarObjetoDespuesDeTiempo());
    }

    // Coroutine que espera y luego muestra el objeto
    private IEnumerator MostrarObjetoDespuesDeTiempo()
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(tiempoDeEspera);

        // Mostrar el objeto
        objetoOculto.SetActive(true);

        // Aquí termina, el objeto se quedará visible permanentemente
    }
}
