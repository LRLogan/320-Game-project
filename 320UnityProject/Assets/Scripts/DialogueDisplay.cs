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
    /// The delay, in seconds, between "next line" inputs (to prevent accidental skipping)
    /// </summary>
    [SerializeField] float delay;

    /// <summary>
    /// List of lines to display in order (for testing).
    /// </summary>
    [SerializeField] string[] lines;

    GameObject dialoguePanel;
    float delayTimer;
    int currentLine = -1;

    // Start is called before the first frame update
    void Start()
    {
        dialoguePanel = dialogueBox.transform.parent.gameObject;
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
            if (!dialoguePanel.activeSelf)
                dialoguePanel.SetActive(true);
            dialogueBox.text = lines[currentLine];
        }
        else if (dialoguePanel.activeSelf)
            dialoguePanel.SetActive(false);

        delayTimer = delay;
    }
}
