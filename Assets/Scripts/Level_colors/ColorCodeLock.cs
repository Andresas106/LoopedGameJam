using UnityEngine;

public class ColorCodeLock : MonoBehaviour
{
    [SerializeField] private Color[] correctCode = new Color[9]; // C�digo correcto con 9 colores
    private Color[] playerCode = new Color[9]; // C�digo introducido por el jugador
    private int currentSlot = 0; // �ndice del slot actual
    public GameObject door; // Asigna tu puerta aqu�
    private Animator doorAnimator;
    public Material YES;
    public Material NO;
    public Material NEGRO;
    public Renderer YESS;
    public Renderer NOO;

    public AudioClip correctSound;  // Sonido para cuando acierta
    public AudioClip wrongSound;    // Sonido para cuando se equivoca
    public AudioClip Puerta;
    private AudioSource audioSource; // El componente AudioSource para reproducir sonidos



    private void Start()
    {
        if (door != null)
            doorAnimator = door.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
            YESS.material = YES;
            NOO.material = NEGRO;
            if (audioSource != null && correctSound != null)
            {
                audioSource.PlayOneShot(correctSound);
                audioSource.PlayOneShot(Puerta);
            }
        }
        else
        {
            Debug.Log("C�digo incorrecto. Int�ntalo de nuevo.");
            YESS.material = NEGRO; // Dejar YES en material original (negro)
            NOO.material = NO;
            if (audioSource != null && wrongSound != null)
            {
                audioSource.PlayOneShot(wrongSound);
            }
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
        Debug.Log("�C�digo correcto! Abriendo puerta...");
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Move");
        }
    }
}

