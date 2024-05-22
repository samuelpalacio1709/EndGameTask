using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CharacterInteractor : MonoBehaviour, IEntityInteractor
{
    [SerializeField] private UnityEvent<IEntityInteractable> onSavedInteractable;
    IEntityInteractable currentInteractable;
    private List<IEntityInteractable> equippedInteractables = new List<IEntityInteractable>();
    private void OnEnable()
    {
        CharacterInput.onInputInteract += Interact;
    }


    private void OnTriggerEnter(Collider other)
    {
        IEntityInteractable interactable;
        if (other.gameObject.TryGetComponent(out interactable))
        {
            currentInteractable = interactable;
            if (CheckIfEntityCanInteract())

            {
                interactable.EnterInteractable();
            }
            else
            {
                interactable.PreventInteraction(this);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IEntityInteractable interactable;
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

            if (CheckIfEntityCanInteract())
            {
                currentInteractable.Interact(this);
            }

        }
    }

    /// <summary>
    /// Check if entity (player) has all interactables required to interact with the selected interactable
    /// </summary>
    /// <returns></returns>
    private bool CheckIfEntityCanInteract()
    {

        if (currentInteractable == null) return false;
        if (currentInteractable.InteractableInfo.onInventory) return false;

        List<InteractableInfo> interatablesInfo = equippedInteractables
                            .Select(interactable => interactable.InteractableInfo).ToList();

        if (interatablesInfo == null) return true;

        return currentInteractable.InteractableInfo.requiredInteractables
                          .All(interactable => interatablesInfo.Contains(interactable));
    }

    /// <summary>
    /// Save the interactable in the inside the entity
    /// </summary>
    /// <param name="interactor"></param>
    /// <param name="interactable"></param>
    public void SaveInteractor(GameObject interactor, IEntityInteractable interactable)
    {
        interactable.InteractableInfo.onInventory = true;
        onSavedInteractable?.Invoke(interactable);
        equippedInteractables.Add(interactable);
        interactor.transform.parent = transform;
        interactor.gameObject.SetActive(false);
    }
}

public interface IEntityInteractor
{
    public void SaveInteractor(GameObject interactor, IEntityInteractable info);
}