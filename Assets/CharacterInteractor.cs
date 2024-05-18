using UnityEngine;

public class CharacterInteractor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IPlayerInteractable interactable;
        if (other.gameObject.TryGetComponent(out interactable))
        {
            interactable.EnterInteractable();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IPlayerInteractable interactable;
        if (other.gameObject.TryGetComponent(out interactable))
        {
            interactable.ExitInteractable();
        }
    }
}
