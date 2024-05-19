using UnityEngine;

public class RotateToMousePosition : MonoBehaviour
{

    void LateUpdate()
    {
        var worldMousePosition = MouseToFloorCoordinates.GetWorldPosition();
        worldMousePosition.y = transform.position.y;

        transform.LookAt(worldMousePosition);

    }
}
