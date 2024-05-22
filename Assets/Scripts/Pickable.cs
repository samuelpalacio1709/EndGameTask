using UnityEngine;

public class Pickable : Interactable
{
    [SerializeField] private string id;
    public override void Interact(IEntityInteractor interactor)
    {
        base.Interact(interactor);
        ExitInteractable();
        interactor.SaveInteractor(this.gameObject, this);
    }


}
