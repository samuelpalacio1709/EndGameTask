using UnityEngine;

public class RotateToMousePosition : MonoBehaviour
{


    void LateUpdate()
    {
        var worldMousePosition = CoordinatesHandler.GetWorldPosition(transform.position);
        worldMousePosition.y = transform.position.y;

        transform.LookAt(worldMousePosition);

    }
}
