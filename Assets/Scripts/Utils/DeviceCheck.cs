using UnityEngine;
using UnityEngine.Events;

public class DeviceCheck : MonoBehaviour
{
    public UnityEvent<bool> onMobileFound;
    public UnityEvent<bool> onOtherDeviceFound;

    private void Start()
    {
        onMobileFound?.Invoke(Application.platform == RuntimePlatform.Android);
        onOtherDeviceFound?.Invoke(Application.platform != RuntimePlatform.Android);

    }
}
