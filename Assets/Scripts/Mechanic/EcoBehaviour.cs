using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcoBehaviour : MonoBehaviour
{

    private bool canInteract = true;
    private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        //SetupEcoAppearance();
        characterController = GetComponent<CharacterController>();
        StartCoroutine(EnableCharacterControllerTemporarily());
    }


    IEnumerator EnableCharacterControllerTemporarily()
    {
        characterController.enabled = true;
        yield return new WaitForSeconds(0.5f);
        characterController.enabled = false;
    }
    /*void SetupEcoAppearance()
    {
        // Cambiar color o añadir efectos visuales al eco
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = new Color(0.2f, 0.6f, 1f, 0.5f); // Azul translúcido
        }

        // (Opcional) Agregar partículas o aura
        // var particles = GetComponentInChildren<ParticleSystem>();
        // if (particles != null) particles.Play();
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (canInteract)
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact(); // Llama al método de interacción de la trampa
            }
        }
    }
}
