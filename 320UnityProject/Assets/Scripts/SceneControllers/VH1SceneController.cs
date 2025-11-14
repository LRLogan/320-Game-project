using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;

public class VH1SceneController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueUIPrefab;
    [SerializeField] private GameObject playerSpawnPoint;
    [SerializeField] private EventSystem eventSystem;
    private Canvas canvas;
    private InventoryManager inventoryUI;

    private void Awake()
    {
        // Find the persistent canvas
        canvas = GameObject.Find("Canvas")?.GetComponent<Canvas>();

        if (canvas == null)
        {
            Debug.LogWarning("No PersistentCanvas found! Make sure one exists before loading this scene.");
            return;
        }

        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        DialogueDisplay dpDisplay = eventSystem.GetComponent<DialogueDisplay>();
        player.dialogueDisplay = dpDisplay;
        player.transform.position = playerSpawnPoint.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        inventoryUI = FindFirstObjectByType<InventoryManager>();
        inventoryUI.RefreshUI();
    }

}
