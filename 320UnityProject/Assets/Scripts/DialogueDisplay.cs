using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DialogueDisplay : MonoBehaviour
{
    /// <summary>
    /// The TMP_Text property of the GameObject that directly displays text.
    /// </summary>
    public TMP_Text dialogueBox;

    /// <summary>
    /// The TMP_Text property of the GameObject that directly displays speaker name text.
    /// </summary>
    public TMP_Text speakerBox;

    /// <summary>
    /// Whether to show the first line as soon as Start is called.
    /// </summary>
    public bool onStart;

    /// <summary>
    /// Whether to lock player movement while dialogue is being displayed.
    /// </summary>
    public bool lockMovement;

    /// <summary>
    /// Action(s) to perform when dialogue is over.
    /// </summary>
    [SerializeField] UnityEvent onEnd;

    /// <summary>
    /// JSON file containing the lines to display.
    /// Format as such: [Speaker]>>[Sprite]>>[Dialogue]
    /// If there is no speaker, simply format as: >>[Sprite]>>[Dialogue]
    /// If there is no sprite, enter "[Sprite]" as "0"
    /// </summary>
    [SerializeField] TextAsset inkScript;

    /// <summary>
    /// List of lines to display in order (for testing).
    /// Format as such: [Speaker]>>[Dialogue]
    /// If there is no speaker, simply format as: >>[Dialogue]
    /// </summary>
    [SerializeField] string[] lines;

    /// <summary>
    /// The delay, in seconds, between "next line" inputs (to prevent accidental skipping)
    /// </summary>
    const float delay = 0.5f;

    public Player playerScript;
    GameObject dialoguePanel;
    GameObject speakerPanel;
    GameObject infoPanel;
    float delayTimer = 0;
    Story inkStory;
    bool paused = false;
    int currentLine = -1;

    float size0;
    bool autoSize0;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindAnyObjectByType<Player>();
        dialoguePanel = dialogueBox.transform.parent.gameObject;
        speakerPanel = speakerBox.transform.parent.gameObject;
        //infoPanel = GetComponent<UIController>().infoBox.transform.parent.gameObject;

        inkStory = new Story(inkScript.text);

        size0 = dialogueBox.fontSize;
        autoSize0 = dialogueBox.enableAutoSizing;

        if (onStart)
            NextLine();
        else if (dialoguePanel.activeSelf)
            dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        delayTimer = Mathf.Max(0, delayTimer - Time.deltaTime);
    }

    public void NextLine(InputAction.CallbackContext context)
    {
        if (delayTimer <= 0)
            NextLine();
    }

    void NextLine()
    {
        if (paused || (!inkStory.canContinue && inkStory.currentChoices.Count <= 0 && !dialoguePanel.activeSelf))
            return;

        dialogueBox.fontSize = size0;
        dialogueBox.enableAutoSizing = autoSize0;

        currentLine++;
        if (inkStory.canContinue)
        {
            if (lockMovement && playerScript.canMove)
                playerScript.canMove = false;

            string line = inkStory.Continue();
            if (line.Length > 0)
            {
                string speaker = speakerBox.text;
                string dialogue = line;
                if (line.Contains(">>"))
                {
                    speaker = line.Substring(0, line.IndexOf(">>"));
                    dialogue = line.Substring(line.IndexOf(">>") + 2);
                }
                if (dialogue.StartsWith('<') && dialogue.Contains('>'))
                {
                    string size = dialogue.Substring(1, dialogue.IndexOf('>') - 1);
                    float sizeF;
                    if (float.TryParse(size, out sizeF))
                    {
                        dialogue = dialogue.Substring(dialogue.IndexOf('>') + 1);

                        dialogueBox.enableAutoSizing = false;
                        dialogueBox.fontSize = sizeF;
                    }
                }

                speakerBox.text = speaker;
                if (speaker.Length == 0)
                    speakerPanel.SetActive(false);
                else if (!speakerPanel.activeSelf)
                    speakerPanel.SetActive(true);

                if (!dialoguePanel.activeSelf)
                    dialoguePanel.SetActive(true);
                dialogueBox.text = dialogue;

                //if (infoPanel.activeSelf)
                    //infoPanel.SetActive(false);
            }
            else if (dialoguePanel.activeSelf)
                dialoguePanel.SetActive(false);
        }
        else if (inkStory.currentChoices.Count > 0)
        {
            List<Choice> choices = inkStory.currentChoices;
            if (choices.Count == 1 && choices[0].text == "0")
            {
                HideDialogue();
                paused = true;
            }
            else
            {

            }
        }
        else
        {
            HideDialogue();

            onEnd.Invoke();
        }

        delayTimer = delay;
    }

    void HideDialogue()
    {
        if (lockMovement && !playerScript.canMove)
            playerScript.canMove = true;

        if (dialoguePanel.activeSelf)
            dialoguePanel.SetActive(false);
    }

    public void ChooseChoiceIndex(int index)
    {
        if (inkStory.currentChoices.Count <= 0)
            return;

        paused = false;
        inkStory.ChooseChoiceIndex(index);
        NextLine();
    }

    public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
}
