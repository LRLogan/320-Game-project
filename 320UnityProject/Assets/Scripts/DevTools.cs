using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DevTools : MonoBehaviour
{
    private DevTools instance;
    private bool devToolsActive = false;
    private string[] textInput = null;
    private Player player;

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject baseItemPrefab;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inputField.onEndEdit.AddListener(HandleInputSubmit);
        inputField.onSelect.AddListener(OnSelectField);
        inputField.onDeselect.AddListener(OnDeselectField);

        player = FindAnyObjectByType<Player>();
    }

    private void OnSelectField(string text)
    {
        devToolsActive = true;
        player.canMove = !devToolsActive;
        //Debug.Log("Input Field Selected - Dev Tools Active: " + devToolsActive);
    }

    private void OnDeselectField(string text)
    {
        devToolsActive = false;
        player.canMove = !devToolsActive;
        //Debug.Log("Input Field Deselected - Dev Tools Active: " + devToolsActive);
    }

    private void HandleInputSubmit(string text)
    {
        // Store the value
        textInput = text.Split(' ');

        // Checking value to apply the correct command
        switch (textInput[0])
        {
            // Load scene
            case "scene":
                DevLoadScene(textInput[1]);
                break;

            // Give item
            case "item":
                DevGiveItem(int.Parse(textInput[1]));
                break;

            default:
                Debug.LogWarning($"{textInput} command not recognized");
                break;
        }


        // Clear input field when done
        inputField.text = string.Empty;
    }

    private void DevLoadScene(string sceneName)
    {
        try
        {
            SceneManager.LoadScene(sceneName);
        }
        catch
        {
            Debug.LogWarning($"{sceneName} not found as a scene");
        }
    }

    private void DevGiveItem(int id)
    {
        GameObject newItem = Instantiate(baseItemPrefab);
        newItem.GetComponent<interactableObject>().id = id;
        player.AddToInventory(newItem);
    }
}
