using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    UIController controller;
    DialogueDisplay dialogue;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += Assign;
    }

    // Start is called before the first frame update
    void Start()
    {
        Assign();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Assign(Scene scene, LoadSceneMode mode) => Assign();
    void Assign()
    {
        controller = GameObject.FindWithTag("UIController").GetComponent<UIController>();
        dialogue = controller.GetComponent<DialogueDisplay>();
    }

    public void NextLine(InputAction.CallbackContext context)
    {
        if (!dialogue)
            return;

        dialogue.OnLineChange(context);
    }
}
