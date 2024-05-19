using UnityEngine;
using UnityEngine.Events;

public class ParticleEventsHandler : MonoBehaviour
{
    [SerializeField] UnityEvent<GameObject> onCollision;
    private void OnParticleCollision(GameObject other)
    {
        onCollision?.Invoke(other);
    }
}
