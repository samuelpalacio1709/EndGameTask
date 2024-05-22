using UnityEngine;

/// <summary>
/// Subscribe to the player input events to call all character animations and movement behaviorss
/// </summary>

public class PlayerManager : EntityManager, IEnemyInteractable
{

    [SerializeField] private CharacterSettings characterSettings;
    [SerializeField] private WeaponController weaponController;


    private void OnEnable()
    {
        if (weaponController != null)
        {
            CharacterInput.onInputAttack += weaponController.Fire;
        }
        CharacterInput.OnInputMovement += characterMovement.ChangePlayerDirection;
        CharacterInput.onInputAttack += characterMovement.SetAttackMovement;
        CharacterInput.OnInputMovement += characterAnimation.UpdateMovementAnimation;
        CharacterInput.onInputAttack += characterAnimation.UpdateAttackAnimation;


    }
    private void OnDisable()
    {
        if (weaponController != null)
        {
            CharacterInput.onInputAttack -= weaponController.Fire;
        }
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
