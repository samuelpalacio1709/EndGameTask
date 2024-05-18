using Player;
using UnityEngine;

/// <summary>
/// Subscribe to the player input events to call all character animations and movement behaviorss
/// </summary>
[RequireComponent(typeof(CharacterAnimation))]
[RequireComponent(typeof(CharacterMovement))]
public class PlayerManager : MonoBehaviour, IEnemyInteractable
{
    private CharacterMovement characterMovement;
    private CharacterAnimation characterAnimation;
    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        characterAnimation = GetComponent<CharacterAnimation>();

    }
    private void OnEnable()
    {
        CharacterInput.OnInputMovement += characterMovement.ChangePlayerDirection;
        CharacterInput.onInputAttack += characterMovement.SetAttackMovement;
        CharacterInput.OnInputMovement += characterAnimation.UpdateMovementAnimation;
        CharacterInput.onInputAttack += characterAnimation.UpdateAttackAnimation;
    }
    private void OnDisable()
    {
        CharacterInput.OnInputMovement -= characterMovement.ChangePlayerDirection;
        CharacterInput.onInputAttack -= characterMovement.SetAttackMovement;
        CharacterInput.OnInputMovement -= characterAnimation.UpdateMovementAnimation;
        CharacterInput.onInputAttack -= characterAnimation.UpdateAttackAnimation;

    }
    private void Update()
    {
        characterMovement.HandlePlayerMovement();

    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact()
    {
    }
}
