using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Interactable Info", menuName = "Data/ new Interactable info", order = 3)]
public class InteractableInfo : ScriptableObject
{
    public string interactableName;
    public Sprite icon;
    public List<InteractableInfo> requiredInteractables;
    public string messageOnInteraction;
    public string messageOnUnableToInteract;
    public bool onInventory = false;
}



