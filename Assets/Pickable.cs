using UnityEngine;

public class Pickable : MonoBehaviour, IPlayerInteractable
{
    [SerializeField] private string Message;
    public void EnterInteractable()
    {
        UIManager.Instance.ShowToastMessage(Message);
    }

    public void ExitInteractable()
    {
        UIManager.Instance.HideToastMessage();

    }

    public void Interact(IEntityInteractor interactor)
    {
        ExitInteractable();
        interactor.SaveInteractor(this.gameObject);


    }
}
