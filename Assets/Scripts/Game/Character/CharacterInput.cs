using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterInput : MonoBehaviour
{
    private PlayerInputActions input;
    public static Action<Vector2> OnInputMovement;
    public static Action<CharacterAttackState, Vector3> onInputAttack;
    public static Action onInputInteract;
    private Coroutine shootingAnimationCouroutine;
    private CharacterAttackState characterState = CharacterAttackState.Rest;
    private CharacterSettings characterSettings;

    private void Awake()
    {
        input = new PlayerInputActions();
        characterSettings = GetComponent<IGameEntity>().GetSettings() as CharacterSettings;
    }
    private void OnEnable()
    {
        input.Enable();
        input.Player.Movement.performed += HandleInputMovementPerformed;
        input.Player.Movement.canceled += HandleInputMovementCanceled;
        input.Player.Attack.performed += HandleInputAttack;
        input.Player.Attack.canceled += HandleInputAttack;
        input.Player.Interact.performed += HandleInputInteract;

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
    private void HandleInputInteract(InputAction.CallbackContext context)
    {
        onInputInteract?.Invoke();
    }
    private void HandleInputAttack(InputAction.CallbackContext context)
    {
        var clickValue = context.ReadValue<float>();

        if ((shootingAnimationCouroutine == null))
        {
            if (clickValue <= 0)
            {
                if (characterState == CharacterAttackState.Aim)
                {
                    characterState = CharacterAttackState.Attack;
                    shootingAnimationCouroutine = StartCoroutine(StopAttackAnimation());
                }

            }
            else
            {

                characterState = CharacterAttackState.Aim;
            }

            SetAttackState(characterState);

        }

    }
    private IEnumerator StopAttackAnimation()
    {
        yield return new WaitForSeconds(characterSettings.attackTime);
        characterState = Mouse.current.leftButton.isPressed ?
                            CharacterAttackState.Aim : CharacterAttackState.Rest;

        SetAttackState(characterState);
        shootingAnimationCouroutine = null;
    }

    private void SetAttackState(CharacterAttackState state)
    {
        onInputAttack?.Invoke(state, MouseToFloorCoordinates.GetWorldPosition());

    }

}

public enum CharacterAttackState
{
    Rest,
    Aim,
    Attack
}

