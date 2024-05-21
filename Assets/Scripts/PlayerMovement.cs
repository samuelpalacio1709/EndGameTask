using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : CharacterMovement
{
    private CharacterController characterController;

    public override void Init()
    {
        base.Init();
        characterController = GetComponent<CharacterController>();
    }
    public override void HandlePlayerMovement()
    {
        base.HandlePlayerMovement();
        MoveCharacter();
    }
    private void MoveCharacter()
    {
        Vector3 movement = inputDirection * characterSettings.movementSpeed * Time.deltaTime;
        characterController.Move(movement);
        characterController.SetYPositionToZero();
    }



}


public static class CharacterControllerExtensions
{
    public static void SetYPositionToZero(this CharacterController characterController)
    {
        if (characterController != null)
        {
            Vector3 newPosition = characterController.transform.position;
            newPosition.y = 0;
            characterController.transform.position = newPosition;
        }
    }
}
