using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class handles all input from the input system
/// </summary>
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
    private Vector2 aimValue = Vector2.zero;
    private float pressedValue = 0;
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
        input.Player.Aim.performed += HandleInputAim;
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
        input.Player.Aim.performed -= HandleInputAim;

        StopAllCoroutines();
    }


    private void HandleInputMovementPerformed(InputAction.CallbackContext context)
    {
        OnInputMovement?.Invoke(context.ReadValue<Vector2>().normalized);
    }
    private void HandleInputMovementCanceled(InputAction.CallbackContext context)
    {
        OnInputMovement?.Invoke(Vector2.zero);
    }
    private void HandleInputInteract(InputAction.CallbackContext context)
    {
        onInputInteract?.Invoke();
    }
    private void HandleInputAim(InputAction.CallbackContext context)
    {
        aimValue = context.ReadValue<Vector2>().normalized;
        CoordinatesHandler.SetAim(aimValue);

    }
    private void HandleInputAttack(InputAction.CallbackContext context)
    {

        pressedValue = context.ReadValue<float>();
        if (gameObject.activeSelf)
        {
            StartCoroutine(ProccessAttack(context));

        }

    }
    /// <summary>
    /// Set the input state to attack
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private IEnumerator ProccessAttack(InputAction.CallbackContext context)
    {
        yield return onFrameEnd;


        //Check if the mouse is over UI on pc

#if !UNITY_ANDROID
                if (!CoordinatesHandler.IsMouseOnWorldTarget()
                    && characterState != CharacterAttackState.Aim)
                    yield break;
#endif


        var clickValue = context.ReadValue<float>();
        pressedValue = clickValue;

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

        characterState = pressedValue > 0 ?
                   CharacterAttackState.Aim : CharacterAttackState.Rest;

        SetAttackState(characterState);
        shootingTimeCoroutine = null;
    }

    private void SetAttackState(CharacterAttackState state)
    {
        onInputAttack?.Invoke(state, CoordinatesHandler
                         .GetWorldPosition(transform.position));

    }

}

public enum CharacterAttackState
{
    Rest,
    Aim,
    Attack
}

