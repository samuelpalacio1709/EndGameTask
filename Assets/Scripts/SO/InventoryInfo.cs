using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player InventoryInfo", menuName = "Player/ new PlayerInventory", order = 4)]

public class InventoryInfo : ScriptableObject
{
    public List<InteractableInfo> interactables;
    public void AddOneItem(IEntityInteractable interactable)
    {
        if (!interactables.Contains(interactable.InteractableInfo))
        {
            interactables.Add(interactable.InteractableInfo);
            onInventoryChange?.Invoke();
        }
    }
    public Action onInventoryChange;
}
