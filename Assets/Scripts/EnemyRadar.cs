using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyRadar : MonoBehaviour
{
    [SerializeField] private GameObject radar;
    [SerializeField] private float radarRadius;
    public Action<IEnemyInteractable> onInteractableFound;
    public Action<IEnemyInteractable> onInteractableLost;
    IEnemyInteractable interactable;

    private void Start()
    {
        if (radar != null)
        {
            radar.transform.localScale = new Vector3(radarRadius, radarRadius, radarRadius);

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out interactable))
        {
            onInteractableFound?.Invoke(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out interactable))
        {
            onInteractableLost?.Invoke(interactable);
        }
    }
}
