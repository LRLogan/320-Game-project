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

    private void Start()
    {
        // Find the persistent canvas
        canvas = GameObject.Find("Canvas")?.GetComponent<Canvas>();

        if (canvas == null)
        {
            Debug.LogWarning("No PersistentCanvas found! Make sure one exists before loading this scene.");
            return;
        }

        GameObject dialogueUIInstance = Instantiate(dialogueUIPrefab, canvas.transform);
        dialogueUIInstance.layer = LayerMask.NameToLayer("UI");
        dialogueUIInstance.SetActive(true);

        GameObject infoPannelInstance = Instantiate(infoPannelPrefab, canvas.transform);
        dialogueUIInstance.layer = LayerMask.NameToLayer("UI");
        dialogueUIInstance.SetActive(true);

        DialogueDisplay dpDisplay = eventSystem.GetComponent<DialogueDisplay>();
        dpDisplay.onStart = true;
        dpDisplay.lockMovement = true;

        // Getting the different text components in the dialogue pannel ans assinging them 
        TextMeshProUGUI[] textsInChild= dialogueUIInstance.GetComponentsInChildren<TextMeshProUGUI>();
        UIController uiController = eventSystem.GetComponent<UIController>();
        dpDisplay.dialogueBox = textsInChild[0];
        dpDisplay.speakerBox = textsInChild[1];
    }
}
