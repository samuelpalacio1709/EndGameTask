using UnityEngine;
using static CharacterInput;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] CharacterSettings characterSettings;
        private CharacterController characterController;
        private Vector3 inputDirection;
        private Vector3 inputAttackDirection;
        private bool isAttacking = false;
        Quaternion movementRotation;


        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        public void HandlePlayerMovement()
        {
            //Player input applied to character
            if ((inputDirection.magnitude > 0))
            {
                MoveCharacter();
                movementRotation = Quaternion.LookRotation(inputDirection);

            }
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
                // Make the transform look at the target position with no smooth
                Vector3 targetPosition = this.inputAttackDirection;
                transform.LookAt(targetPosition);
            }
            else
            {
                // Make the transform look at the target position with  smooth

                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                 movementRotation, Time.deltaTime * characterSettings.turnSpeed);
            }


        }
        public void SetAttackMovement(CharacterAttackState state, Vector3 inputAttackDirection)
        {
            isAttacking = state == CharacterAttackState.Attack;
            this.inputAttackDirection = inputAttackDirection;

        }

        public void ChangePlayerDirection(Vector2 direction)
        {
            inputDirection = new Vector3(direction.x, 0, direction.y);
        }
    }

}
