using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    public bool followPlayer = false;
    private Transform target;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        if (target != null && followPlayer)
        {
            var targetPosition = target.position;
            transform.position = targetPosition + offset;

        }
    }
}
