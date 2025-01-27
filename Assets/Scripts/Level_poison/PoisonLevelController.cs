using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonLevelController : MonoBehaviour
{
    public float initialTimer = 10f;  // Tiempo base de supervivencia inicial
    public float gasReductionRate = 0.5f;  // Tasa de reducci�n de gas por ventilador
    private float currentTimer;  // Temporizador actual en cada vida
    private float accumulatedGasReduction = 0f;  // Reducci�n acumulada del gas (progreso persistente)
    private bool isPlayerAlive = true;

    private PlayerController playerController;

    void Start()
    {
        // Inicializar el temporizador considerando la reducci�n acumulada
        ResetTimer();
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (isPlayerAlive)
        {
            // Reducir el temporizador conforme pasa el tiempo
            currentTimer -= Time.deltaTime;

            if (currentTimer <= 0)
            {
                isPlayerAlive = false;

                // Llamar a la funci�n de muerte del jugador
                playerController.Die();

                // Reiniciar el temporizador
                ResetTimer();
            }

            // Actualizar efectos visuales del gas
            //UpdateGasEffect();
        }
    }

    public void ActivateVentilator(float bonusTime)
    {
        // Actualizar la reducci�n acumulada y el temporizador actual
        accumulatedGasReduction += gasReductionRate;
        currentTimer += bonusTime;

        // Asegurarse de no superar los l�mites del gas
        accumulatedGasReduction = Mathf.Clamp(accumulatedGasReduction, 0f, 1f);

        // Opcional: Actualizar efectos visuales y/o de audio
        //UpdateGasEffect();
    }

    private void ResetTimer()
    {
        // Reiniciar el temporizador considerando la reducci�n acumulada del gas
        currentTimer = initialTimer + (accumulatedGasReduction * initialTimer);
        isPlayerAlive = true;
    }

    /*private void UpdateGasEffect()
    {
        // Representar visualmente el nivel de gas
        RenderSettings.fogDensity = Mathf.Lerp(0.05f, 0.01f, accumulatedGasReduction);
    }*/
}
