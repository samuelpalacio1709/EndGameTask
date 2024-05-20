using UnityEngine;

public class CharacterInteractor : MonoBehaviour, IEntityInteractor
{
    IPlayerInteractable currentInteractable;
    private void OnEnable()
    {
        CharacterInput.onInputInteract += Interact;
    }
    private void OnTriggerEnter(Collider other)
    {
        IPlayerInteractable interactable;
        if (other.gameObject.TryGetComponent(out interactable))
        {
            currentInteractable = interactable;
            interactable.EnterInteractable();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IPlayerInteractable interactable;
        if (other.gameObject.TryGetComponent(out interactable))
        {
            if (currentInteractable == interactable)
            {
                interactable.ExitInteractable();
                currentInteractable = null;
            }


        }
    }
    private void Interact()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact(this);

        }
    }
    public void SaveInteractor(GameObject interactor)
    {
        interactor.transform.parent = transform;
        interactor.gameObject.SetActive(false);
    }
}

public interface IEntityInteractor
{
    public void SaveInteractor(GameObject interactor);
}