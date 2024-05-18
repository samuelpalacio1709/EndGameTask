using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterInput : MonoBehaviour
{
    private PlayerInputActions input;
    public static Action<Vector2> OnInputMovement;
    public static Action<float> onInputAttack;
    private void Awake()
    {
        input = new PlayerInputActions();
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += HandleInputMovementPerformed;
        input.Player.Movement.canceled += HandleInputMovementCanceled;
        input.Player.Attack.performed += HandleInputAttack;
        input.Player.Attack.canceled += HandleInputAttack;

    }
    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= HandleInputMovementPerformed;
        input.Player.Movement.canceled -= HandleInputMovementCanceled;
        input.Player.Attack.performed -= HandleInputAttack;
    }

    private void HandleInputMovementPerformed(InputAction.CallbackContext context)
    {
        OnInputMovement?.Invoke(context.ReadValue<Vector2>());
    }
    private void HandleInputMovementCanceled(InputAction.CallbackContext context)
    {
        OnInputMovement?.Invoke(Vector2.zero);
    }
    private void HandleInputAttack(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<float>();
        onInputAttack?.Invoke(value);
    }


}

