using UnityEngine;
using System.Collections;  // Necesario para las Coroutines

public class ButtonInteraction : MonoBehaviour
{

    public Camera playerCamera; // La cámara del jugador
    private GameObject selectedButton = null; // Objeto seleccionado por el raycast
    public float raycastDistance = 5f; // Distancia del raycast para interactuar
    private InputManager inputManager; // Para gestionar la interacción con "E"

    private bool hasInteracted = false;      // Variable para controlar si ya se ha interactuado

    // Aquí puedes tener los colores predefinidos para el código, lo que sea necesario
    //public Color[] colorCode;  // Colores que el jugador debe seleccionar para el código (puedes configurarlo desde el Inspector)

    void Start()
    { // Usamos la cámara principal
        inputManager = GetComponent<InputManager>();  // El InputManager debería manejar las teclas de entrada
    }

    void Update()
    {
        RaycastInteraction();  // Raycast para detectar el color con el que se está interactuando

        // Solo permite la interacción si el jugador tiene algo seleccionado y presiona "E"
        if (selectedButton != null && inputManager.IsInteractPressed && !hasInteracted)
        {
            InteractWithButton(selectedButton); // Interactuar con el color
            hasInteracted=true;
        }

        if(!inputManager.IsInteractPressed)
        {
            hasInteracted = false;
        }
    }

    // Función para lanzar el raycast y seleccionar el objeto correspondiente
    void RaycastInteraction()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition); // Lanzamos un ray desde el mouse
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance))  // Asegúrate de que solo los botones de colores sean detectados
        {
            if (hit.collider.CompareTag("Interactable"))  // Solo los objetos con este tag pueden ser seleccionados
            {
                if (selectedButton != hit.collider.gameObject)  // Evitar repetir la selección
                {
                    selectedButton = hit.collider.gameObject;
                }
            }
        }
    }

    // Función para interactuar con el objeto de color seleccionado
    private void InteractWithButton(GameObject button)
    {
        // Puedes obtener el color que tenga el botón y hacer lo que necesites
        Color buttonColor = button.GetComponent<Renderer>().material.color;
        FindObjectOfType<ColorCodeLock>().InteractWithColorObject(buttonColor);
    }

    //        
}


// 