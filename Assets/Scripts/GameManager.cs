using UnityEngine;

/// <summary>
/// This class holds the reference to the game components
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public GameObject mainCamera;
    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.gameObject;
        }
    }
}
