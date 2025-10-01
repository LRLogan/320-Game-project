using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    /// The delay, in seconds, between "next line" inputs (to prevent accidental skipping)
    /// </summary>
    [SerializeField] float delay;

    /// <summary>
    /// List of lines to display in order (for testing).
    /// Format as such: [Speaker]>>[Dialogue]
    /// If there is no speaker, simply format as: >>[Dialogue]
    /// </summary>
    [SerializeField] string[] lines;

    GameObject dialoguePanel;
    GameObject speakerPanel;
    float delayTimer;
    int currentLine = -1;

    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel = dialogueBox.transform.parent.gameObject;
        speakerPanel = speakerBox.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        delayTimer = Mathf.Max(0, delayTimer - Time.deltaTime);
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && delayTimer <= 0)
            NextLine();
    }

    void NextLine()
    {
        currentLine++;
        if (currentLine >= 0 && currentLine < lines.Length)
        {
            string line = lines[currentLine];
            string speaker = "";
            string dialogue = line;
            if (line.Contains(">>"))
            {
                speaker = line.Substring(0, line.IndexOf(">>"));
                dialogue = line.Substring(line.IndexOf(">>") + 2);
            }

            if (speaker.Length == 0)
                speakerPanel.SetActive(false);
            else
            {
                if (!speakerPanel.activeSelf)
                    speakerPanel.SetActive(true);
                speakerBox.text = speaker;
            }

            if (!dialoguePanel.activeSelf)
                dialoguePanel.SetActive(true);
            dialogueBox.text = dialogue;
        }
        else if (dialoguePanel.activeSelf)
            dialoguePanel.SetActive(false);

        delayTimer = delay;
    }
}
