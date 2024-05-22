using UnityEngine;

/// <summary>
/// This class holds the reference to  game components
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public CameraController cameraController;
    private GameObject maincamera;

    public GameObject Maincamera
    {
        get => maincamera;
        private set => maincamera = value;
    }

    private void Awake()
    {
        if (cameraController.camera == null)
        {
            Maincamera = Camera.main.gameObject;
        }
    }


}
