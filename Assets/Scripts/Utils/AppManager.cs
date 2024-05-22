using UnityEngine;

public class AppManager : MonoBehaviour
{
    private void Awake() => Application.targetFrameRate = 120;
    public void ExitApp()
    {
        Application.Quit();
    }
}
