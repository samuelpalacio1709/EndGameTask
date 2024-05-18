using UnityEngine;

public class MouseToFloorCoordinates : MonoBehaviour
{
    private Camera mainCamera;
    private static Plane plane = new Plane(Vector3.up, 0);

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    /// <summary>
    ///Get the mouse position converted to world position
    /// </summary>
    /// <returns></returns>
    public static Vector3 GetWorldPosition()
    {
        Vector3 worldPosition = Vector3.zero;
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
        }
        return worldPosition;
    }
}
