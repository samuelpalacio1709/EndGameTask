using UnityEngine;
using UnityEngine.EventSystems;

public class CoordinatesHandler : MonoBehaviour
{
    private static Camera mainCamera;
    private static Plane plane = new Plane(Vector3.up, 0);
    private static Vector2 aimValue = Vector2.zero;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    /// <summary>
    ///Get the mouse position converted to world position
    /// </summary>
    /// <returns></returns>
    /// 
    public static void SetAim(Vector2 aim)
    {
        aimValue = aim;
    }
    public static Vector3 GetWorldPosition(Vector3 currentPosition)
    {
        if (aimValue == Vector2.zero)
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
        else
        {
            return currentPosition + new Vector3(aimValue.x, currentPosition.y, aimValue.y);
        }

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
