using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryItemPrefab;
    private int selectedSlot = -1;

    // Fill with all slots in the UI
    public InventorySlot[] inventorySlots;

    private void ChangeSelectedSlot(int newValue)
    {
        if(selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    // Searches for a spot in the inventory
    public bool AddItem(Item item)
    {
        InventorySlot curSlot;
        InventoryItem itemInSlot;

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            curSlot = inventorySlots[i];
            itemInSlot = curSlot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null) { SpawnNewItem(item, curSlot); return true; }
        }
        return false;
    }

    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItem = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem invItem = newItem.GetComponent<InventoryItem>();
        invItem.InitialiseItem(item);
    }
}
