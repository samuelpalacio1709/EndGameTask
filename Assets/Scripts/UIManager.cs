using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TMP_Text toastMessage;
    [SerializeField] private InventoryInfo inventory;
    [SerializeField] private List<UISlot> weaponSlots = new List<UISlot>();
    [SerializeField] private List<UISlot> consumablesSlots = new List<UISlot>();
    private void OnEnable()
    {
        if (inventory != null)
        {
            inventory.onInventoryChange += UpdateInventoryView;
        }
    }
    private void OnDisable()
    {
        if (inventory != null)
        {
            inventory.onInventoryChange -= UpdateInventoryView;
        }
    }
    private void Start()
    {
        UpdateInventoryView();
    }
    private void UpdateInventoryView()
    {
        for (int i = 0; i < inventory.interactables.Count; i++)
        {
            var inventoryItem = inventory.interactables[i] as InventoryInteractableInfo;
            switch (inventoryItem.itemType)
            {
                case InventoryInteractableInfo.ItemType.Weapon:
                    weaponSlots[i].ShowIcon(inventoryItem.icon);
                    break;
                case InventoryInteractableInfo.ItemType.Consumable:
                    consumablesSlots[i].ShowIcon(inventoryItem.icon);
                    break;
                default:
                    break;
            }
        }

    }

    public void ShowToastMessage(string message)
    {

        if (toastMessage != null)
        {
            toastMessage.text = message;
        }
    }
    public void HideToastMessage()
    {
        if (toastMessage != null)
        {
            toastMessage.text = "";
        }

    }

}
