using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Scene controller that attaches scene-specific UI elements to the persistent canvas when the scene loads.
/// </summary>
public class DeadsGreyBoxSceneController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueUIPrefab;
    [SerializeField] private GameObject infoPannelPrefab;
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

        // Dialogue display 
        GameObject dialogueUIInstance = Instantiate(dialogueUIPrefab, canvas.transform);
        dialogueUIInstance.layer = LayerMask.NameToLayer("UI");
        dialogueUIInstance.SetActive(true);

        DialogueDisplay dpDisplay = eventSystem.GetComponent<DialogueDisplay>();
        dpDisplay.onStart = true;
        dpDisplay.lockMovement = true;

        // Getting the different text components in the dialogue pannel ans assinging them 
        TextMeshProUGUI[] textsInChild = dialogueUIInstance.GetComponentsInChildren<TextMeshProUGUI>();
        dpDisplay.dialogueBox = textsInChild[1];
        dpDisplay.speakerBox = textsInChild[0];

        // Info pannel / UI controller
        GameObject infoPannelInstance = Instantiate(infoPannelPrefab, canvas.transform);
        infoPannelInstance.layer = LayerMask.NameToLayer("UI");
        infoPannelInstance.SetActive(true);

        UIController uiController = eventSystem.GetComponent<UIController>();
        uiController.infoBox = infoPannelInstance.GetComponentInChildren<TextMeshProUGUI>();

        // Player dialogue reference
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.dialogueDisplay = dpDisplay;
        player.transform.GetChild(0).GetComponent<interactArea>().InfoSetup(infoPannelInstance);
        //infoPannelInstance.SetActive(false);
    }

    private void Start()
    {
        inventoryUI = FindFirstObjectByType<InventoryManager>();
        inventoryUI.RefreshUI();
    }
}
