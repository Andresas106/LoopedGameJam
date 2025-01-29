using UnityEngine;

/// <summary>
/// Controla la animación de un objeto interactuable (torus, etc.).
/// </summary>
public class ObjetoInteractivo : MonoBehaviour, IInteractuable
{
    public Animator animator;
    public string triggerAnimacion;

    public void Interactuar()
    {
        if (animator != null)
        {
            animator.SetTrigger(triggerAnimacion);
            Debug.Log($"{gameObject.name} activó la animación: {triggerAnimacion}");
        }
    }
}
