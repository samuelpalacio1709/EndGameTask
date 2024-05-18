using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    public bool followPlayer = false;
    private Transform target;
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            SetTarget(player.transform);
        }
    }
    private void SetTarget(Transform target)
    {
        this.target = target;

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
