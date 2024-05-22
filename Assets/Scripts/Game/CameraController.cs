using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private string targetTag;
    [SerializeField] private float cameraFollowSpeed;
    [SerializeField] public Camera camera;
    public bool followTarget = false;
    private Transform target;
    private float initialSpeed;
    private void Awake()
    {
        initialSpeed = cameraFollowSpeed;
        if (camera == null)
        {
            camera = Camera.main;
        }
    }
    void Start()
    {
        SetTarget(targetTag);
        StartCoroutine(ShowInitialAnimation());
    }
    private void SetTarget(string tag)
    {
        var player = GameObject.FindGameObjectWithTag(tag);
        if (player != null)
        {
            this.target = player.transform;

        }

    }

    void LateUpdate()
    {
        if (target != null && followTarget)
        {
            var targetPosition = target.position + offset;

            camera.transform.position = Vector3.Lerp(camera.transform.position,
                                                targetPosition, Time.deltaTime * cameraFollowSpeed);
        }
    }

    public IEnumerator ShowInitialAnimation()
    {
        cameraFollowSpeed = 0;
        yield return new WaitForSeconds(0.3f);
        while (cameraFollowSpeed != initialSpeed)
        {
            yield return new WaitForSeconds(0.1f);
            cameraFollowSpeed += 0.1f;
        }
        cameraFollowSpeed = initialSpeed;

    }
}
