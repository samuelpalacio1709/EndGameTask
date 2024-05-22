using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Implementation of IEntityInteractable. Handles all the logic when the player sends interactions.
/// </summary>
public class Interactable : MonoBehaviour, IEntityInteractable
{
    [SerializeField] private InteractableInfo interactableInfo;
    [SerializeField] private UnityEvent onEntered;
    [SerializeField] private UnityEvent onExit;
    [SerializeField] private UnityEvent onHoverExit;
    [SerializeField] private UnityEvent onInteract;
    public InteractableInfo InteractableInfo => interactableInfo;

    public virtual void EnterInteractable()
    {
        onEntered?.Invoke();
        UIManager.Instance.ShowToastMessage(InteractableInfo.messageOnInteraction);
    }

    public virtual void ExitInteractable()
    {
        onExit?.Invoke();
        UIManager.Instance.HideToastMessage();

    }

    public void HoverExit()
    {
        onHoverExit?.Invoke();
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
