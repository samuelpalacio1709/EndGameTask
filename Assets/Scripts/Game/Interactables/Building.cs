
using UnityEngine;
public class Building : Interactable
{

    public override void ExitInteractable()
    {
        base.ExitInteractable();
        Debug.Log("Exit building");
    }
}
