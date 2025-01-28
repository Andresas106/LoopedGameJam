using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonLevelController : MonoBehaviour
{
    public float initialTimer = 4f;  // Tiempo base de supervivencia inicial
    public float currentTimer;  // Temporizador actual en cada vida
    private bool isPlayerAlive = true;
    private bool isInLabyrinth = false;

    private PlayerController playerController;

    void Start()
    {
        // Inicializar el temporizador considerando la reducción acumulada
        ResetTimer();
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (isPlayerAlive && isInLabyrinth)
        {
            // Reducir el temporizador conforme pasa el tiempo
            currentTimer -= Time.deltaTime;

            if (currentTimer <= 0)
            {
                isPlayerAlive = false;

                // Llamar a la función de muerte del jugador
                playerController.Die();

                Invoke(nameof(ResetTimer), 1f);
            }

            // Actualizar efectos visuales del gas
            //UpdateGasEffect();
        }
    }

    public void ActivateVentilator(float bonusTime)
    {
        // Actualizar la reducción acumulada y el temporizador actual
        currentTimer += bonusTime;
        initialTimer = initialTimer + bonusTime;

        // Asegurarse de no superar los límites del gas

        // Opcional: Actualizar efectos visuales y/o de audio
        //UpdateGasEffect();
    }

    private void ResetTimer()
    {
        // Reiniciar el temporizador considerando la reducción acumulada del gas
        currentTimer = initialTimer;
        isPlayerAlive = true;
        isInLabyrinth = false;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInLabyrinth = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInLabyrinth = false;
            currentTimer = initialTimer;
        }
    }

    /*private void UpdateGasEffect()
    {
        // Representar visualmente el nivel de gas
        RenderSettings.fogDensity = Mathf.Lerp(0.05f, 0.01f, accumulatedGasReduction);
    }*/
}
