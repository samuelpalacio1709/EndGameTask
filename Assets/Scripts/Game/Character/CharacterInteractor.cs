using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class manages all interactable objects triggered by the entity
/// </summary>
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
            if (CheckIfEntityCanInteract(interactable))

            {
                currentInteractable = interactable;
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
                interactable.HoverExit();

            }

            interactable.ExitInteractable();



        }
    }
    private void Interact()
    {
        if (currentInteractable != null)
        {

            if (CheckIfEntityCanInteract(currentInteractable))
            {
                currentInteractable.Interact(this);
            }

        }
    }

    /// <summary>
    /// Check if entity (player) has all interactables required to interact with the selected interactable
    /// </summary>
    /// <returns></returns>
    private bool CheckIfEntityCanInteract(IEntityInteractable interactable)
    {

        if (interactable == null) return false;
        if (interactable.InteractableInfo.onInventory) return false;

        List<InteractableInfo> interatablesInfo = equippedInteractables
                            .Select(interactable => interactable.InteractableInfo).ToList();

        if (interatablesInfo == null) return true;

        return interactable.InteractableInfo.requiredInteractables
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