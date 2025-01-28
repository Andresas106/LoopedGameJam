using UnityEngine;

public class ColorCodeLock : MonoBehaviour
{
    [SerializeField] private Color[] correctCode = new Color[9]; // C�digo correcto con 9 colores
    private Color[] playerCode = new Color[9]; // C�digo introducido por el jugador
    private int currentSlot = 0; // �ndice del slot actual
    public GameObject door; // Asigna tu puerta aqu�
    private Animator doorAnimator;

    private void Start()
    {
        if (door != null)
            doorAnimator = door.GetComponent<Animator>();
    }

    // Llamada por los objetos interactivos en el entorno
    public void InteractWithColorObject(Color objectColor)
    {
        if (currentSlot < playerCode.Length)
        {
            // A�ade el color al c�digo del jugador
            playerCode[currentSlot] = objectColor;
            currentSlot++;

            Debug.Log($"Color seleccionado: {objectColor}. Slot {currentSlot}/{playerCode.Length}");

            // Si el jugador ha seleccionado 9 colores, verifica el c�digo
            if (currentSlot == playerCode.Length)
            {
                CheckCode();
                currentSlot = 0; // Reinicia para un nuevo intento
            }
        }
    }

    private void CheckCode()
    {
        bool isCorrect = true;

        // Compara el c�digo del jugador con el c�digo correcto
        for (int i = 0; i < correctCode.Length; i++)
        {
            if (playerCode[i] != correctCode[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            OpenDoor();
        }
        else
        {
            Debug.Log("C�digo incorrecto. Int�ntalo de nuevo.");
        }
    }

    private void OpenDoor()
    {
        Debug.Log("�C�digo correcto! Abriendo puerta...");
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Move");
        }
    }
}

