using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] CharacterSettings characterSettings;
        [SerializeField] GameObject characterVisuals;
        private CharacterController characterController;
        private Vector3 inputDirection;
        private Vector3 inputAttackDirection;
        private bool isAttacking = false;
        private bool rotateToAttack = false;
        Quaternion movementRotation;


        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            characterVisuals.transform.localEulerAngles = characterSettings.rotationOffset;
        }

        public void HandlePlayerMovement()
        {
            //Player input applied to character
            if ((inputDirection.magnitude > 0))
            {
                movementRotation = Quaternion.LookRotation(inputDirection);

            }
            MoveCharacter();
            RotateCharacter();
        }

        private void MoveCharacter()
        {
            Vector3 movement = inputDirection * characterSettings.movementSpeed * Time.deltaTime;
            characterController.Move(movement);
        }

        private void RotateCharacter()
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
                // Make the transform look at the target position with  smooth
                characterVisuals.transform.localEulerAngles = characterSettings.rotationOffset;
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                 movementRotation, Time.deltaTime * characterSettings.turnSpeed);
            }


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

}
