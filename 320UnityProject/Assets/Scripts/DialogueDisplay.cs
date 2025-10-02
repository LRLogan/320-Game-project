using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DialogueDisplay : MonoBehaviour
{
    /// <summary>
    /// The TMP_Text property of the GameObject that directly displays text.
    /// </summary>
    [SerializeField] TMP_Text dialogueBox;

    /// <summary>
    /// The TMP_Text property of the GameObject that directly displays speaker name text.
    /// </summary>
    [SerializeField] TMP_Text speakerBox;

    /// <summary>
    /// Whether to show the first line as soon as Start is called.
    /// </summary>
    [SerializeField] bool onStart;

    /// <summary>
    /// Whether to lock player movement while dialogue is being displayed.
    /// </summary>
    [SerializeField] bool lockMovement;

    /// <summary>
    /// Action(s) to perform when dialogue is over.
    /// </summary>
    [SerializeField] UnityEvent onEnd;

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
    float delayTimer = 0;
    int currentLine = -1;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindAnyObjectByType<Player>();
        dialoguePanel = dialogueBox.transform.parent.gameObject;
        speakerPanel = speakerBox.transform.parent.gameObject;

        if (onStart)
            NextLine();
        else if (dialoguePanel.activeSelf)
            dialoguePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        delayTimer = Mathf.Max(0, delayTimer - Time.deltaTime);
        if (Input.GetMouseButtonDown(0) && delayTimer <= 0)
            NextLine();
    }

    void NextLine()
    {
        currentLine++;
        if (currentLine >= 0 && currentLine < lines.Length)
        {
            if (lockMovement && playerScript.canMove)
                playerScript.canMove = false;

            string line = lines[currentLine];
            if (line.Length > 0)
            {
                string speaker = speakerBox.text;
                string dialogue = line;
                if (line.Contains(">>"))
                {
                    speaker = line.Substring(0, line.IndexOf(">>"));
                    dialogue = line.Substring(line.IndexOf(">>") + 2);
                }

                speakerBox.text = speaker;
                if (speaker.Length == 0)
                    speakerPanel.SetActive(false);
                else if (!speakerPanel.activeSelf)
                    speakerPanel.SetActive(true);

                if (!dialoguePanel.activeSelf)
                    dialoguePanel.SetActive(true);
                dialogueBox.text = dialogue;
            }
            else if (dialoguePanel.activeSelf)
                dialoguePanel.SetActive(false);
        }
        else
        {
            if (lockMovement && !playerScript.canMove)
                playerScript.canMove = true;

            if (dialoguePanel.activeSelf)
                dialoguePanel.SetActive(false);

            if (currentLine == lines.Length)
                onEnd.Invoke();
        }

        delayTimer = delay;
    }

    public void LoadScene(int index) => SceneManager.LoadScene(index);
}
