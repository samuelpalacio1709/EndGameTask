using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour
    {
        private CharacterController characterController;
        public float movementSpeed = 5f;
        private Vector3 inputDirection;
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
            Vector3 movement = inputDirection * movementSpeed * Time.deltaTime;
            characterController.Move(movement);
        }

        private void ChangePlayerDirection(Vector2 direction)
        {
            Debug.Log(direction);
            inputDirection = new Vector3(direction.x, 0, direction.y);
        }
    }

}
