using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DevTools : MonoBehaviour
{
    private DevTools instance;
    private bool devToolsActive;
    private string[] textInput = null;

    [SerializeField] private TMP_InputField inputField;


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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleInputSubmit(string text)
    {
        // Store the value
        textInput = text.Split(' ');

        // Checking value to apply the correct command
        switch (textInput[0])
        {
            case "scene":
                DevLoadScene(textInput[1]);
                break;

            default:
                Debug.Log($"{textInput} command not recognized");
                break;
        }


        // Clear input field when done
        inputField.text = string.Empty;
    }

    private void DevLoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
