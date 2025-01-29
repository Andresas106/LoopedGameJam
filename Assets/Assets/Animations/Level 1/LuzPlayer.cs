using System.Collections;
using UnityEngine;

public class PlayerDeathLight : MonoBehaviour
{
    public GameObject lightPrefab; // Prefab de la luz que se generar� al morir
    public float lightDuration = 5f; // Duraci�n de la luz antes de desaparecer
    public float fadeOutTime = 1f; // Tiempo que tarda en desvanecerse la luz
    InputManager inputManager;
    private bool isDead = false; // Estado del jugador

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        if (inputManager.IsDiePressed)
        {
            Die();
        }
    }

    // M�todo para manejar la muerte del jugador
    public void Die()
    {
        if (isDead) return;
        isDead = true;

        // Crear la luz en el lugar donde muri� el jugador
        if (lightPrefab != null)
        {
            GameObject deathLight = Instantiate(lightPrefab, transform.position, Quaternion.identity);
            StartCoroutine(FadeAndDestroyLight(deathLight));
        }

        // Aqu� puedes agregar cualquier otra l�gica que quieras realizar al morir.
        Debug.Log("El jugador ha muerto.");
    }

    // Corrutina para desvanecer y destruir la luz despu�s de cierto tiempo
    private IEnumerator FadeAndDestroyLight(GameObject lightObject)
    {
        Light lightComponent = lightObject.GetComponent<Light>();
        float startIntensity = lightComponent.intensity;

        // Esperar el tiempo de duraci�n antes de comenzar a desvanecer
        yield return new WaitForSeconds(lightDuration);

        float timer = 0f;
        while (timer < fadeOutTime)
        {
            timer += Time.deltaTime;
            lightComponent.intensity = Mathf.Lerp(startIntensity, 0, timer / fadeOutTime);
            yield return null;
        }

        Destroy(lightObject); // Destruir el objeto de luz despu�s del desvanecimiento
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detectar si el jugador colisiona con algo que lo mata
        if (other.CompareTag("Matable"))
        {
            Die();
        }
    }
}
