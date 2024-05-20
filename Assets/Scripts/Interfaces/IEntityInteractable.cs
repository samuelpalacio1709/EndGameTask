public interface IEntityInteractable
{
    public void ExitInteractable();

    public void EnterInteractable();
    public void Interact(IEntityInteractor interactor);
    public void PreventInteraction(IEntityInteractor interactor);
    public InteractableInfo InteractableInfo { get; }

}
