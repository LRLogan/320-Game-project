using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class FrogVilleSceneController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject playerSpawnPoint;
    private GameObject playerInstance = null;

    [SerializeField] private InventoryManager inventoryUI;
    [SerializeField] private Camera camera;
    [SerializeField] private interactArea interactArea;
    public static bool firstLoad = true;
    private GameManager gameManager;

    [SerializeField] private GameObject dialogueUIPrefab;
    [SerializeField] private GameObject infoPannelPrefab;
    [SerializeField] private GameObject choiceParentPrefab;
    [SerializeField] private EventSystem eventSystem;
    private Canvas canvas;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        try
        {
            playerInstance = FindAnyObjectByType<Player>().gameObject;

            if (firstLoad)
            {
                playerInstance.transform.position = playerSpawnPoint.transform.position;
                firstLoad = false;
            }
            else
            {
                playerInstance.transform.position = playerInstance.GetComponent<Player>().posInOverworldBeforeSceneChange;
            }
        }
        catch
        {

            if (playerInstance == null)
            {
                Debug.Log("Player creation called in scene controller");
                playerInstance = Instantiate(playerPrefab, playerSpawnPoint.transform.position, Quaternion.identity);
               
            }
        }
        inventoryUI = FindAnyObjectByType<InventoryManager>();
        playerInstance.GetComponent<Player>().inventoryUI = inventoryUI;
        Debug.Log(playerInstance.name);
        if (camera.GetComponent<sCameraInterior>() != null)
        {
            camera.GetComponent<sCameraInterior>().player = playerInstance.transform;
        }
        else
        {
            camera.GetComponent<sSimpleCamera>().player = playerInstance.transform;
        }

        // Find the persistent canvas
        canvas = GameObject.Find("Canvas")?.GetComponent<Canvas>();

        if (canvas == null)
            Debug.LogWarning("No PersistentCanvas found! Make sure one exists before loading this scene.");
        else
        {
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

            UIController uiController = eventSystem.GetComponent<UIController>();
            uiController.infoBox = infoPannelInstance.GetComponentInChildren<TextMeshProUGUI>();
            dpDisplay.InfoSetup(infoPannelInstance);

            // Player dialogue reference
            playerInstance.GetComponent<Player>().dialogueDisplay = dpDisplay;
            playerInstance.transform.GetChild(0).GetComponent<interactArea>().dialogueDisplay = dpDisplay;
        }

        interactArea.playerScript = playerInstance.GetComponent<Player>();
        inventoryUI.player = playerInstance.GetComponent<Player>();
       
    }

    /// <summary>
    /// Actions that need to happen at start time rather than awake
    /// </summary>
    private void Start()
    {
        inventoryUI.RefreshUI();
        playerInstance.GetComponent<Player>().rotateControls = false;
    }

}
