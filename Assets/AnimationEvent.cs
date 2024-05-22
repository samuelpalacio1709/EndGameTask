using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    public UnityEvent onAnimationEvent;

    public void TriggerAnimationEvent()
    {
        onAnimationEvent?.Invoke();
    }
}
