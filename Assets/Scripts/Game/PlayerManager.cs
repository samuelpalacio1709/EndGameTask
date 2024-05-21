using UnityEngine;

/// <summary>
/// Subscribe to the player input events to call all character animations and movement behaviorss
/// </summary>
[RequireComponent(typeof(CharacterAnimation))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerManager : MonoBehaviour, IEnemyInteractable, IGameEntity
{
    private PlayerMovement characterMovement;
    private CharacterAnimation characterAnimation;
    [SerializeField] private CharacterSettings characterSettings;
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private HealthController healthController;

    private void Awake()
    {
        characterMovement = GetComponent<PlayerMovement>();
        characterAnimation = GetComponent<CharacterAnimation>();

    }
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
        CharacterInput.onInputAttack -= weaponController.Fire;


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
    public IEntitySettings GetSettings()
    {
        return characterSettings;
    }

    public void RecieveDamage(float damage)
    {
        if (healthController != null)
        {
            healthController.HandleDamage(damage);
        }
    }

    public void Respawn()
    {
        gameObject.SetActive(false);
    }

    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
}
