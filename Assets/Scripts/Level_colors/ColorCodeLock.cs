using UnityEngine;

public class ColorCodeLock : MonoBehaviour
{
    [SerializeField] private Color[] correctCode = new Color[9]; // Código correcto con 9 colores
    private Color[] playerCode = new Color[9]; // Código introducido por el jugador
    private int currentSlot = 0; // Índice del slot actual
    public GameObject door; // Asigna tu puerta aquí
    private Animator doorAnimator;
    public Material YES;
    public Material NO;
    public Material NEGRO;
    public Renderer YESS;
    public Renderer NOO;

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
            // Añade el color al código del jugador
            playerCode[currentSlot] = objectColor;
            currentSlot++;

            Debug.Log($"Color seleccionado: {objectColor}. Slot {currentSlot}/{playerCode.Length}");

            // Si el jugador ha seleccionado 9 colores, verifica el código
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

        // Compara el código del jugador con el código correcto
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
            YESS.material = YES;
            NOO.material = NEGRO;
        }
        else
        {
            Debug.Log("Código incorrecto. Inténtalo de nuevo.");
            YESS.material = NEGRO; // Dejar YES en material original (negro)
            NOO.material = NO;
        }

        Invoke("ResetMaterials", 2f);
    }

    void ResetMaterials()
    {
        YESS.material = NEGRO;
        NOO.material = NEGRO;
    }

    private void OpenDoor()
    {
        Debug.Log("¡Código correcto! Abriendo puerta...");
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Move");
        }
    }
}

