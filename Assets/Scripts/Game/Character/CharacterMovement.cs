using UnityEngine;


/// <summary>
/// This class handles movement for both player and enemies
/// </summary>
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] GameObject characterVisuals;

    protected CharacterSettings characterSettings;
    protected Vector3 inputDirection;
    protected Vector3 inputAttackDirection;

    private bool isAttacking = false;
    private bool rotateToAttack = false;
    private Quaternion movementRotation;

    private void Awake()
    {
        Init();
    }

    public virtual void HandlePlayerMovement()
    {
        //Player input applied to character
        if ((inputDirection.magnitude > 0))
        {
            movementRotation = Quaternion.LookRotation(inputDirection);

        }
        RotateCharacter();
    }
    public virtual void Init()
    {
        characterSettings = GetComponent<IGameEntity>().GetSettings() as CharacterSettings;
        characterVisuals.transform.localEulerAngles = characterSettings.rotationOffset;
    }

    public void RotateCharacter()
    {
        if (isAttacking)
        {
            if (!rotateToAttack)
            {
                rotateToAttack = true;
                // Make the transform look at the target position with no smooth
                Vector3 targetPosition = this.inputAttackDirection;
                transform.LookAt(targetPosition);
                movementRotation = transform.rotation;
                characterVisuals.transform.localEulerAngles = characterSettings.rotationOffsetOnAttack;
            }

        }
        else
        {
            ResetCharacterVisualRotation();
            // Make the transform look at the target position with  smooth
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                             movementRotation, Time.deltaTime * characterSettings.turnSpeed);
        }


    }

    public void ResetCharacterVisualRotation()
    {
        characterVisuals.transform.localEulerAngles = characterSettings.rotationOffset;

    }
    public void SetAttackMovement(CharacterAttackState state, Vector3 inputAttackDirection)
    {
        isAttacking = state == CharacterAttackState.Attack;
        rotateToAttack = false;
        this.inputAttackDirection = inputAttackDirection;

    }

    public void ChangePlayerDirection(Vector2 direction)
    {
        inputDirection = new Vector3(direction.x, 0, direction.y);
    }
}




