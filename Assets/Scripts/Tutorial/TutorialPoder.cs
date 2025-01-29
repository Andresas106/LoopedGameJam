using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;  // Necesario para usar el Input System

public class TutorialPoder : MonoBehaviour
{
    public PlayerController poder;    // Referencia al controlador del jugador
    public TextMeshProUGUI texto;         // El texto que aparecerá cuando el jugador tenga poder
    public InputManager inputManager; // Referencia al InputManager

    void Update()
    {
        // Verifica si el jugador tiene poder
        if (poder.havePower)
        {
            texto.enabled = true;  // Muestra el texto
        }

        // Verifica si se ha presionado la tecla R o Triángulo (botón correspondiente del gamepad)
        if (inputManager.IsDiePressed)
        {
            OcultarTexto();  // Oculta el texto cuando se presiona la tecla R o Triángulo
        }
    }

    // Método que se llama para ocultar el texto
    private void OcultarTexto()
    {
        texto.enabled = false;  // Oculta el texto
    }
}
