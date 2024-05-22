using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Listens for particle collision
/// </summary>
public class ParticleEventsHandler : MonoBehaviour
{
    [SerializeField] UnityEvent<GameObject> onCollision;
    private void OnParticleCollision(GameObject other)
    {
        onCollision?.Invoke(other);
    }
}
