using UnityEngine;
using UnityEngine.Events;


public class Building : MonoBehaviour, IPlayerInteractable
{
    [SerializeField] UnityEvent onEntered;
    [SerializeField] UnityEvent onExit;
    [SerializeField] UnityEvent onInteract;

    public void ExitInteractable()
    {
        onExit?.Invoke();
    }

    public void EnterInteractable()
    {
        onEntered?.Invoke();
    }

    public void Interact(IEntityInteractor interactor)
    {
        onInteract?.Invoke();
    }
}
