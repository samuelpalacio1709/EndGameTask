using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Handles all UI updates
/// </summary>
public class UIManager : Singleton<UIManager>
{
    [Header("Toast Message")]

    [SerializeField] private TMP_Text toastMessage;

    [Header("Inventory")]

    [SerializeField] private InventoryInfo inventory;
    [SerializeField] private List<UISlot> weaponSlots = new List<UISlot>();
    [SerializeField] private List<UISlot> consumablesSlots = new List<UISlot>();

    [Header("Match Info settings")]
    [SerializeField] private TMP_Text matchInfoText;
    [SerializeField] private float matchInfoDuration;

    private void OnEnable()
    {
        if (inventory != null)
        {
            inventory.onInventoryChange += UpdateInventoryView;
        }
        EntityManager.onEntityKilled += ShowMatchInfo;
    }

    private void ShowMatchInfo(string info, bool playerDead)
    {
        matchInfoText.text = info;
        StartCoroutine(SetTextTimer(matchInfoText, matchInfoDuration));
    }
    private void OnDisable()
    {
        if (inventory != null)
        {
            inventory.onInventoryChange -= UpdateInventoryView;
        }
        EntityManager.onEntityKilled -= ShowMatchInfo;

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

    public IEnumerator SetTextTimer(TMP_Text label, float time)
    {
        yield return new WaitForSeconds(time);
        label.text = "";
    }

}
