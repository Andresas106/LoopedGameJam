using UnityEngine;
using TMPro;  // Necesario para usar TextMeshPro
using System.Collections;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent;  // Usamos TextMeshProUGUI en lugar de Text
    public string fullText;  // El texto completo que queremos mostrar
    public float typingSpeed = 0.1f;  // Velocidad con la que se "escribe"
    public float startDelay = 0f;  // Retraso en segundos antes de empezar la animación

    private void Start()
    {
        // Comienza la animación de escritura después del retraso
        StartCoroutine(WriteTextWithDelay());
    }

    private IEnumerator WriteTextWithDelay()
    {
        // Espera el tiempo de retraso antes de comenzar
        yield return new WaitForSeconds(startDelay);

        // Inicia la escritura del texto
        textComponent.text = "";  // Limpia el texto antes de empezar
        foreach (char letter in fullText)
        {
            textComponent.text += letter;  // Añade una letra
            yield return new WaitForSeconds(typingSpeed);  // Espera antes de añadir la siguiente letra
        }
    }
}
