using UnityEngine;
using UnityEngine.EventSystems;

public class MouseToFloorCoordinates : MonoBehaviour
{
    private static Camera mainCamera;
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
        if (mainCamera == null) return Vector3.zero;
        Vector3 worldPosition = Vector3.zero;
        float distance;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
        }
        return worldPosition;
    }
    /// <summary>
    /// Check if mouse is hovering UI or a gameobject
    /// </summary>
    /// <returns></returns>
    public static bool IsMouseOnWorldTarget()
    {
        return !EventSystem.current.IsPointerOverGameObject();

    }
}
