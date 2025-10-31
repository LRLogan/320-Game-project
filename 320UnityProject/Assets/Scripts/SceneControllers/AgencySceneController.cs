using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class AgencySceneController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject playerSpawnPoint;
    private GameObject playerInstance = null;

    [SerializeField] private InventoryManager inventoryUI;
    [SerializeField] private Camera camera;
    [SerializeField] private interactArea interactArea;
    [SerializeField] private GameObject pauseMenu;
    private GameManager gameManager;

    [SerializeField] private GameObject dialogueUIPrefab;
    [SerializeField] private GameObject infoPannelPrefab;
    [SerializeField] private EventSystem eventSystem;
    private Canvas canvas;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        try
        {
            playerInstance = FindAnyObjectByType<Player>().gameObject;
            Debug.Log(playerInstance.GetComponent<Player>().posBeforeSceneChange);
            playerInstance.transform.position = playerInstance.GetComponent<Player>().posBeforeSceneChange;
        }
        catch
        {

            if (playerInstance == null)
            {
                Debug.Log("Player creation called in scene controller");
                playerInstance = Instantiate(playerPrefab, playerSpawnPoint.transform.position, Quaternion.identity);

            }
        }
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
            playerInstance.GetComponent<Player>().dialogueDisplay = dpDisplay;
            playerInstance.transform.GetChild(0).GetComponent<interactArea>().InfoSetup(infoPannelInstance);
        }

        //interactArea.playerScript = playerInstance.GetComponent<Player>();
        inventoryUI.player = playerInstance.GetComponent<Player>();

        // Setting up the quit game om click
        gameManager.pauseMenu = pauseMenu;
        InputAction openMenuAction = playerInstance.GetComponent<PlayerInput>().actions["OpenMenu"];

        openMenuAction.performed += gameManager.OnOpenMenu;
    }
        // Start is called before the first frame update
    void Start()
    {
        inventoryUI.RefreshUI();
    }
}
