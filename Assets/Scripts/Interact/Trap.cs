using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Destroy(gameObject); // Destruye la trampa
    }
}
