using FischlWorks_FogWar;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField] private csFogWar fogWar;

    private const int autopsyValue = 0;
    [SerializeField] private TextAsset autopsyScript;
    private const int letterValue = 1;
    [SerializeField] private TextAsset letterScript;
    private const int doneValue = 1;
    [SerializeField] private TextAsset doneScript;
    private const int houseValue = 1;
    [SerializeField] private TextAsset[] houseScripts;
    [SerializeField] private int[] houseIds;
    [SerializeField] private interactableObject autopsyObject;
    [SerializeField] private SceneWarpTrigger[] villageDoors;

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
        if (inventoryUI != null)
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

            // Check if doors need to be locked
            if (gameManager.ContainsDialogue(autopsyScript) < autopsyValue
                || gameManager.ContainsDialogue(doneScript) >= doneValue)
            {
                if (gameManager.ContainsDialogue(doneScript) >= doneValue)
                    dpDisplay.pathChoice = 2;
                foreach (SceneWarpTrigger door in villageDoors)
                    door.locked = true;
            }

            if (gameManager.ContainsDialogue(letterScript) < letterValue)
            {
                dpDisplay.pathChoice = 0;
                autopsyObject.dialogue = "The trash can is full";
                autopsyObject.isEvent = false;
            }
            else
            {
                dpDisplay.pathChoice = 1;
                autopsyObject.isDialogue = true;
                autopsyObject.isEvent = true;
            }
        }

        interactArea.playerScript = playerInstance.GetComponent<Player>();
        inventoryUI.player = playerInstance.GetComponent<Player>();

        fogWar.AddFogRevealer(new csFogWar.FogRevealer(playerInstance.transform, 20, false));
    }

    /// <summary>
    /// Actions that need to happen at start time rather than awake
    /// </summary>
    private void Start()
    {
        inventoryUI.RefreshUI();
        playerInstance.GetComponent<Player>().rotateControls = false;
    }

    public void UnlockDoors()
    {
        if (gameManager.ContainsDialogue(autopsyScript) >= autopsyValue)
            return;

        foreach (SceneWarpTrigger door in villageDoors)
            door.locked = false;
        eventSystem.GetComponent<DialogueDisplay>().pathChoice = -1;
    }

    public void HouseCounter()
    {
        Debug.Log("checking houses");

        int counter = 0;
        foreach (TextAsset house in houseScripts)
        {
            if (gameManager.ContainsDialogue(house) >= houseValue)
                counter++;
        }
        Debug.Log("counter: " + counter);
        if (counter >= houseScripts.Length)
            LockDoors();
        /* List<GameObject> inventory = playerInstance.GetComponent<Player>().GetInventory();
        for (int i = 0; i < inventory.Count; i++)
        {
            if (houseIds.Contains(inventory[i].GetComponent<interactableObject>().id))
                counter++;
        }
        if (counter >= houseIds.Length)
            LockDoors(); */
    }

    private void LockDoors()
    {
        Debug.Log("checking to lock doors");
        if (gameManager.ContainsDialogue(doneScript) >= doneValue)
            return;

        Debug.Log("locking doors");

        gameManager.nextScenePath = "2report";
        DialogueDisplay dpDisplay = eventSystem.GetComponent<DialogueDisplay>();
        dpDisplay.pathChoice = 2;
        foreach (SceneWarpTrigger door in villageDoors)
            door.locked = true;
        dpDisplay.ChoosePathString("1finish");
    }

    public void GetClothes() => gameManager.gotClothes = true;
}
