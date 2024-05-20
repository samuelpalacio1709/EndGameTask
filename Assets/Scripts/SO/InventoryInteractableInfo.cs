
using UnityEngine;

[CreateAssetMenu(fileName = "Interactable Info", menuName = "Data/ new InventoryInfo Interactable info", order = 5)]

public class InventoryInteractableInfo : InteractableInfo
{
    public ItemType itemType;
    public enum ItemType
    {
        Weapon,
        Consumable
    }
}
