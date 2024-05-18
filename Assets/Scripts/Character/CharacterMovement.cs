using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float turnSpeed = 15f;
        private CharacterController characterController;
        private Vector3 inputDirection;
        Quaternion targetRotation;
        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }
        private void OnEnable()
        {
            CharacterInput.OnInputMovement += ChangePlayerDirection;
        }
        private void OnDisable()
        {
            CharacterInput.OnInputMovement -= ChangePlayerDirection;
        }
        private void Update()
        {
            //Player input applied to character
            if ((inputDirection.magnitude > 0))
            {
                MoveCharacter();
                targetRotation = Quaternion.LookRotation(inputDirection);

            }
            RotateCharacter();

        }
        private void MoveCharacter()
        {
            Vector3 movement = inputDirection * movementSpeed * Time.deltaTime;
            characterController.Move(movement);
        }

        private void RotateCharacter()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                 targetRotation, Time.deltaTime * turnSpeed);

        }

        private void ChangePlayerDirection(Vector2 direction)
        {
            inputDirection = new Vector3(direction.x, 0, direction.y);
        }
    }

}
