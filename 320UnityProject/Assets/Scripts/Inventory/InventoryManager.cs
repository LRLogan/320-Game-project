using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    InventoryManager inventoryUIInstance;

    public Player player;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private Transform inventoryGrid; // parent grid layout

    private List<InventoryItem> uiItems = new List<InventoryItem>();

    private void Awake()
    {
        if (inventoryUIInstance == null)
        {
            inventoryUIInstance = this;
            DontDestroyOnLoad(gameObject.transform.root.gameObject);
        }
        else if (inventoryUIInstance != this)
        {
            Destroy(gameObject);
        }
    }

    public void RefreshUI()
    {
        // Clear old UI items (but not slots)
        foreach (Transform child in inventoryGrid)
        {
            if (!child.GetComponent<InventorySlot>())
            {
                Destroy(child.gameObject);
            }
            else
            {
                // Clear any items inside slots before repopulating
                foreach (Transform item in child)
                {
                    Destroy(item.gameObject);
                }
            }
        }

        uiItems.Clear();

        // Create UI item for each GameObject in player's inventory
        foreach (GameObject obj in player.GetInventory())
        {
            interactableObject itemData = obj.GetComponent<interactableObject>();
            Transform emptySlot = GetFirstEmptySlot();

            if (emptySlot != null)
            {
                GameObject newUIItem = Instantiate(inventoryItemPrefab, emptySlot);
                InventoryItem uiItemScript = newUIItem.GetComponent<InventoryItem>();
                uiItemScript.InitialiseItem(itemData);
                uiItems.Add(uiItemScript);
            }
            else
            {
                Debug.LogWarning("No empty inventory slot available!");
            }
        }
    }

    private Transform GetFirstEmptySlot()
    {
        foreach (Transform slot in inventoryGrid)
        {
            if (slot.GetComponent<InventorySlot>() && slot.childCount == 0)
            {
                return slot;
            }
        }
        return null; // No empty slot
    }

    public void SyncInventoryOrder()
    {
        player.GetInventory().Clear();

        foreach (Transform slot in inventoryGrid)
        {
            if (slot.childCount > 0)
            {
                InventoryItem uiItem = slot.GetChild(0).GetComponent<InventoryItem>();
                if (uiItem != null && uiItem.item != null)
                {
                    player.GetInventory().Add(uiItem.item.gameObject);
                }
            }
        }
    }
}
