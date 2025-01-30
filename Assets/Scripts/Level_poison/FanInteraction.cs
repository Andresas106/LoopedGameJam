using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fanInteraction : MonoBehaviour
{
    public Camera playerCamera; // La c�mara del jugador
    private GameObject selectedButton = null; // Objeto seleccionado por el raycast
    public float raycastDistance = 5f; // Distancia del raycast para interactuar
    private InputManager inputManager; // Para gestionar la interacci�n con "E"
    public AudioClip buttonSound; // El clip de audio que quieres reproducir

    private bool hasInteracted = false;      // Variable para controlar si ya se ha interactuado

    // Aqu� puedes tener los colores predefinidos para el c�digo, lo que sea necesario
    //public Color[] colorCode;  // Colores que el jugador debe seleccionar para el c�digo (puedes configurarlo desde el Inspector)

    void Start()
    { // Usamos la c�mara principal
        inputManager = GetComponent<InputManager>();  // El InputManager deber�a manejar las teclas de entrada
    }

    void Update()
    {
        RaycastInteraction();  // Raycast para detectar el color con el que se est� interactuando

        // Solo permite la interacci�n si el jugador tiene algo seleccionado y presiona "E"
        if (selectedButton != null && inputManager.IsInteractPressed && !hasInteracted)
        {
            InteractWithButton(selectedButton); // Interactuar con el color
            hasInteracted = true;
        }

        // Resetear la interacci�n cuando la tecla se suelte o el jugador deje de mirar el objeto
        if (!inputManager.IsInteractPressed)
        {
            hasInteracted = false;
        }
    }

    // Funci�n para lanzar el raycast y seleccionar el objeto correspondiente
    void RaycastInteraction()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition); // Lanzamos un ray desde el mouse
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance))  // Aseg�rate de que solo los botones de colores sean detectados
        {
            if (hit.collider.CompareTag("Interactable"))  // Solo los objetos con este tag pueden ser seleccionados
            {
                if (selectedButton != hit.collider.gameObject)  // Evitar repetir la selecci�n
                {
                    selectedButton = hit.collider.gameObject;
                    
                }
            }
            else
            {
                selectedButton = null;
            }
        }
    }

    // Funci�n para interactuar con el objeto de color seleccionado
    private void InteractWithButton(GameObject button)
    {
        if (selectedButton != null)
        {
            // Puedes obtener el color que tenga el bot�n y hacer lo que necesites
            Animator animator = button.GetComponent<Animator>();
            if(animator != null)
            {
                animator.SetTrigger("Move");
                button.tag = "Untagged";
            }
            

            if (buttonSound != null)
            {
                AudioSource.PlayClipAtPoint(buttonSound, button.transform.position);
            }
            //FindObjectOfType<PlayerInteraction>().interactionText.enabled = false;
            FindObjectOfType<PoisonLevelController>().ActivateVentilator(8f);
        }
    }
}
