using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine;

public class VH1SceneController : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private GameObject dialogueUIPrefab;
    [SerializeField] private GameObject infoPannelPrefab;
    [SerializeField] private GameObject choiceParentPrefab;
    [SerializeField] private GameObject playerSpawnPoint;
    [SerializeField] private EventSystem eventSystem;
    private Canvas canvas;
    private InventoryManager inventoryUI;

    [SerializeField] int ownedId = -1;
    [SerializeField] List<GameObject> disableIfOwned;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        // Find the persistent canvas
        canvas = GameObject.Find("Canvas")?.GetComponent<Canvas>();

        if (canvas == null)
        {
            Debug.LogWarning("No PersistentCanvas found! Make sure one exists before loading this scene.");
            return;
        }

        // Dialogue display
        GameObject dialogueUIInstance = Instantiate(dialogueUIPrefab, canvas.transform);
        dialogueUIInstance.layer = LayerMask.NameToLayer("UI");
        dialogueUIInstance.SetActive(true);

        DialogueDisplay dpDisplay = eventSystem.GetComponent<DialogueDisplay>();
        dpDisplay.onStart = false;
        dpDisplay.lockMovement = true;

        GameObject choiceParent = Instantiate(choiceParentPrefab, canvas.transform);
        dpDisplay.choiceParent = choiceParent.transform;

        dpDisplay.gameManager = gameManager;
        dpDisplay.alreadySeen = gameManager.ContainsDialogue(dpDisplay.inkScript);

        // Getting the different text components in the dialogue pannel and assigning them
        TextMeshProUGUI[] textsInChild = dialogueUIInstance.GetComponentsInChildren<TextMeshProUGUI>();
        dpDisplay.dialogueBox = textsInChild[1];
        dpDisplay.speakerBox = textsInChild[0];

        // Info pannel / UI controller
        GameObject infoPannelInstance = Instantiate(infoPannelPrefab, canvas.transform);
        infoPannelInstance.layer = LayerMask.NameToLayer("UI");
        infoPannelInstance.SetActive(true);
        dpDisplay.InfoSetup(infoPannelInstance);

        
        // Player dialogue reference
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.dialogueDisplay = dpDisplay;
        player.transform.GetChild(0).GetComponent<interactArea>().dialogueDisplay = dpDisplay;
        player.transform.position = playerSpawnPoint.transform.position;

        /* if (ownedId >= 0 && disableIfOwned.Count > 0)
        {
            List<GameObject> inventory = player.GetInventory();
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].GetComponent<interactableObject>().id == ownedId)
                {
                    foreach (GameObject obj in disableIfOwned)
                        obj.SetActive(false);
                }
            }
        } */
        if (disableIfOwned.Count > 0 && gameManager.gotClothes)
        {
            foreach (GameObject obj in disableIfOwned)
                obj.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inventoryUI = FindFirstObjectByType<InventoryManager>();
        inventoryUI.RefreshUI();
    }

    public void SetNextScenePath(string path)
    {
        if (eventSystem.GetComponent<DialogueDisplay>().alreadySeen >= 0)
            return;
        gameManager.nextScenePath = path;
    }
}
