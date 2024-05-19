using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private string targetTag;
    [SerializeField] private float cameraFollowSpeed;
    public bool followTarget = false;
    private Transform target;
    void Start()
    {
        var player = GameObject.FindGameObjectWithTag(targetTag);
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
        if (target != null && followTarget)
        {
            var targetPosition = target.position + offset;

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraFollowSpeed);
        }
    }
}
