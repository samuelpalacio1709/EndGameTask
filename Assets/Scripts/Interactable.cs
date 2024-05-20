using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IEntityInteractable
{
    [SerializeField] private InteractableInfo interactableInfo;
    [SerializeField] private UnityEvent onEntered;
    [SerializeField] private UnityEvent onExit;
    [SerializeField] private UnityEvent onInteract;
    public InteractableInfo InteractableInfo => interactableInfo;

    public virtual void EnterInteractable()
    {
        onEntered?.Invoke();
        UIManager.Instance.ShowToastMessage(InteractableInfo.messageOnInteraction);
    }

    public virtual void ExitInteractable()
    {
        Debug.Log("eXIT");
        onExit?.Invoke();
        UIManager.Instance.HideToastMessage();

    }

    public virtual void Interact(IEntityInteractor interactor)
    {
        onInteract?.Invoke();
    }

    public virtual void PreventInteraction(IEntityInteractor interactor)
    {
        UIManager.Instance.ShowToastMessage(InteractableInfo.messageOnUnableToInteract);
    }
}
