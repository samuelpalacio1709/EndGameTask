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
    private Coroutine shootingTimeCoroutine;
    private CharacterAttackState characterState = CharacterAttackState.Rest;
    private CharacterSettings characterSettings;
    private WaitForEndOfFrame onFrameEnd = new WaitForEndOfFrame();
    private void Awake()
    {
        characterSettings = GetComponent<IGameEntity>().GetSettings() as CharacterSettings;
    }
    private void OnEnable()
    {
        input = new PlayerInputActions();
        input.Enable();
        input.Player.Movement.performed += HandleInputMovementPerformed;
        input.Player.Movement.canceled += HandleInputMovementCanceled;
        input.Player.Attack.performed += HandleInputAttack;
        input.Player.Attack.canceled += HandleInputAttack;
        input.Player.Interact.performed += HandleInputInteract;
        SetAttackState(CharacterAttackState.Rest);
        shootingTimeCoroutine = null;

    }
    private void OnDisable()
    {
        input.Disable();
        input.Player.Movement.performed -= HandleInputMovementPerformed;
        input.Player.Movement.canceled -= HandleInputMovementCanceled;
        input.Player.Attack.performed -= HandleInputAttack;
        input.Player.Attack.canceled -= HandleInputAttack;
        input.Player.Interact.performed -= HandleInputInteract;
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
        StartCoroutine(ProccessAttack(context));

    }

    private IEnumerator ProccessAttack(InputAction.CallbackContext context)
    {
        yield return onFrameEnd;

        //Check if the mouse is over UI
        if (!MouseToFloorCoordinates.IsMouseOnWorldTarget()
        && characterState != CharacterAttackState.Aim)
            yield break;

        var clickValue = context.ReadValue<float>();

        if ((shootingTimeCoroutine == null))
        {
            if (clickValue <= 0)
            {
                if (characterState == CharacterAttackState.Aim)
                {
                    characterState = CharacterAttackState.Attack;
                    shootingTimeCoroutine = StartCoroutine(StopAttack());
                }

            }
            else
            {

                characterState = CharacterAttackState.Aim;
            }

            SetAttackState(characterState);

        }
    }
    private IEnumerator StopAttack()
    {
        yield return new WaitForSeconds(characterSettings.attackTime);
        characterState = Mouse.current.leftButton.isPressed ?
                            CharacterAttackState.Aim : CharacterAttackState.Rest;

        SetAttackState(characterState);
        shootingTimeCoroutine = null;
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

