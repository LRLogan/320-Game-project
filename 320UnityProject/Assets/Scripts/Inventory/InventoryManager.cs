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

            // This will keep the entire canvas
            DontDestroyOnLoad(gameObject.transform.root.gameObject);
        }
        else if (inventoryUIInstance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }

    public void RefreshUI()
    {
        // Clear old UI elements
        foreach (Transform child in inventoryGrid)
        {
            Destroy(child.gameObject);
        }
        uiItems.Clear();

        // Create UI item for each GameObject in player's inventory
        foreach (GameObject obj in player.GetInventory())
        {
            interactableObject itemData = obj.GetComponent<interactableObject>();
            GameObject newUIItem = Instantiate(inventoryItemPrefab, inventoryGrid);
            InventoryItem uiItemScript = newUIItem.GetComponent<InventoryItem>();
            uiItemScript.InitialiseItem(itemData);

            uiItems.Add(uiItemScript);
        }
    }

    // Method to update Player inventory order based on UI
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
