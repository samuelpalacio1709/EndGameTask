using UnityEngine;

public class RotateToMousePosition : MonoBehaviour
{

    void LateUpdate()
    {
        var worldMousePosition = MouseToFloorCoordinates.GetWorldPosition();
        transform.LookAt(worldMousePosition);

    }
}
