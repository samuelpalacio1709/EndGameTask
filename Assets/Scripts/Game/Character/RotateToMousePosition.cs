using UnityEngine;



public class RotateToMousePosition : MonoBehaviour
{

    void LateUpdate()
    {
        LookAtMousePosition();
    }

    /// <summary>
    /// Set the current game object to look at mouse position converted to world position
    /// </summary>
    private void LookAtMousePosition()
    {
        var worldMousePosition = CoordinatesHandler.GetWorldPosition(transform.position);
        worldMousePosition.y = transform.position.y;

        transform.LookAt(worldMousePosition);
    }
}
