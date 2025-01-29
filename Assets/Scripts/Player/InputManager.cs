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

    void Awake()
    {
        input = new InputCharacter();

        input.Player.Move.started += onMovementInput;
        input.Player.Move.canceled += onMovementInput;
        input.Player.Move.performed += onMovementInput;

        input.Player.Run.started += onRunInput;
        input.Player.Run.canceled += onRunInput;

        input.Player.Jump.started += onJumpInput;
        input.Player.Jump.canceled += onJumpInput;

        input.Player.Look.performed += onLookInput;
        input.Player.Look.canceled += onLookInput;

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
        CurrentMovementInput = context.ReadValue<Vector2>();
    }

    private void onLookInput(InputAction.CallbackContext context)
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

    // Método público para cambiar el estado de IsDiePressed
    public void SetDiePressed(bool value)
    {
        IsDiePressed = value;
    }
}
