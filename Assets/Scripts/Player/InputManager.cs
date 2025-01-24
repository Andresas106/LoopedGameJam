using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    InputCharacter input;

    public Vector2 CurrentMovementInput { get; private set; }
    public bool IsRunPressed { get; private set; }
    public bool IsJumpPressed { get; private set; }
    public bool IsDiePressed { get; private set; }
    public bool IsInteractPressed { get; private set; }
    public Vector2 CurrentMouseDelta { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        input = new InputCharacter();

        //Esto se ejecuta cuando el personaje pulsa AWSD o el left stick del gamepad
        input.Player.Move.started += onMovementInput;
        input.Player.Move.canceled += onMovementInput;
        input.Player.Move.performed += onMovementInput;
        //Esto se ejecuta cuando el personaje pulsa shift Izq o el boton del gamepad
        input.Player.Run.started += onRunInput;
        input.Player.Run.canceled += onRunInput;
        //Esto se ejecuta cuando el personaje pulsa espacio o el boton del gamepad
        input.Player.Jump.started += onJumpInput;
        input.Player.Jump.canceled += onJumpInput;

        // Detectar movimiento del ratón
        input.Player.Look.performed += onLookInput; // Nuevo evento
        input.Player.Look.canceled += onLookInput; // Detener movimiento del ratón

        input.Player.Die.started += onDieInput;
        input.Player.Die.canceled += onDieInput;


        input.Player.Interact.started += onInteractInput;
        input.Player.Interact.canceled += onInteractInput;



    }

    private void onInteractInput(InputAction.CallbackContext context)
    {
        IsInteractPressed = context.ReadValueAsButton();
    }

    private void onJumpInput(InputAction.CallbackContext context)
    {
        IsJumpPressed = context.ReadValueAsButton();
    }

    private void onRunInput(InputAction.CallbackContext context)
    {
        IsRunPressed = context.ReadValueAsButton();
    }

    private void onMovementInput(InputAction.CallbackContext context)
    {
        //Al detectar los inputs del teclado o gamepad se tendran dos valores de x e y
        CurrentMovementInput = context.ReadValue<Vector2>();
    }

    private void onLookInput(InputAction.CallbackContext context) // Método para capturar movimiento del ratón
    {
        CurrentMouseDelta = context.ReadValue<Vector2>();
    }

    private void onDieInput(InputAction.CallbackContext context)
    {
        IsDiePressed = context.ReadValueAsButton();
    }

    private void OnEnable()
    {
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }
}
