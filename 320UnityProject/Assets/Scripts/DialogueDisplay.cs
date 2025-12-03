using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    /// Image sprite to be shown when there is a displayed speaker name.
    /// </summary>
    public Sprite speakerSprite;

    /// <summary>
    /// Image sprite to be shown when there is no displayed speaker name.
    /// </summary>
    public Sprite noSpeakerSprite;

    /// <summary>
    /// The Transform under which to instantiate choice buttons as children.
    /// </summary>
    public Transform choiceParent;

    /// <summary>
    /// The prefab object for a button displayed during a choice.
    /// </summary>
    public GameObject choicePrefab;

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
    [SerializeField] public TextAsset inkScript;

    /// <summary>
    /// List of lines to display in order (for testing).
    /// Format as such: [Speaker]>>[Dialogue]
    /// If there is no speaker, simply format as: >>[Dialogue]
    /// </summary>
    [SerializeField] string[] lines;

    /// <summary>
    /// The delay, in seconds, between "next line" inputs (to prevent accidental skipping)
    /// </summary>
    const float delay = 0.25f;

    /// <summary>
    /// The vertical space between the centers of displayed choice buttons.
    /// </summary>
    const float choiceDistance = 60;

    public GameManager gameManager;
    public int alreadySeen = -1;
    int seeing = -1;

    public Player playerScript;
    GameObject dialoguePanel;
    GameObject speakerPanel;
    Image panelImage;
    
    /// <summary>
    /// The number of seconds infoPanel is visible until it starts fading out.
    /// </summary>
    private const float infoTime = 2;

    /// <summary>
    /// The number of seconds it takes for infoPanel to completely disappear once it starts fading out.
    /// </summary>
    private const float infoFadeTime = 1;

    public GameObject infoPanel;
    private TextMeshProUGUI infoBox;
    private Image infoImage;
    private float infoAlpha;
    private float infoTimer = 0;
    private bool infoTiming = false;

    float delayTimer = 0;
    Story inkStory;
    bool choosing = false;
    bool paused = false;
    int currentLine = -1;

    float size0;
    bool autoSize0;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneUnloaded += DestroyPanels;

        playerScript = FindAnyObjectByType<Player>();
        dialoguePanel = dialogueBox.transform.parent.gameObject;
        speakerPanel = speakerBox.transform.parent.gameObject;
        panelImage = dialoguePanel.GetComponent<Image>();
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

        if (infoTiming && infoPanel != null)
        {
            if (infoTimer > 0)
            {
                infoTimer = Mathf.Max(0, infoTimer - Time.deltaTime);
                if (infoTimer <= infoFadeTime)
                {
                    infoImage.color -= new Color(0, 0, 0, Time.deltaTime * infoAlpha / infoFadeTime);
                    infoBox.color -= new Color(0, 0, 0, Time.deltaTime / infoFadeTime);
                }
            }
            else if (infoPanel.activeSelf)
            {
                infoImage.color += new Color(0, 0, 0, infoAlpha - infoImage.color.a);
                infoBox.color += new Color(0, 0, 0, 1 - infoBox.color.a);
                infoPanel.SetActive(false);
                infoTiming = false;
            }
        }
    }

    public void NextLine(InputAction.CallbackContext context)
    {
        if (delayTimer <= 0)
            NextLine();
    }

    void NextLine()
    {
        if ((seeing < 0 && alreadySeen >= Mathf.Max(0, inkStory.currentChoices.Count - 1)) || (seeing >= 0 && alreadySeen >= seeing))
        {
            dialoguePanel.SetActive(false);
            return;
        }

        if (seeing < 0)
        {
            seeing = alreadySeen + 1;
            Debug.Log("seeing: " + seeing);
            if (inkStory.currentChoices.Count > 0)
                inkStory.ChooseChoiceIndex(seeing);
                //inkStory.ChoosePath(inkStory.currentChoices[seeing].path);
        }

        if (choosing || paused || (!inkStory.canContinue && inkStory.currentChoices.Count <= 0 && !dialoguePanel.activeSelf))
            return;

        dialogueBox.fontSize = size0;
        dialogueBox.enableAutoSizing = autoSize0;

        //currentLine++;
        if (inkStory.canContinue)
        {
            if (lockMovement && playerScript.canMove)
                playerScript.canMove = false;

            string line = inkStory.Continue();

            Debug.Log("line: " + line);
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
                {
                    speakerPanel.SetActive(false);
                    panelImage.sprite = noSpeakerSprite;
                }
                else if (!speakerPanel.activeSelf)
                {
                    speakerPanel.SetActive(true);
                    panelImage.sprite = speakerSprite;
                }

                if (!dialoguePanel.activeSelf)
                    dialoguePanel.SetActive(true);
                dialogueBox.text = dialogue;

                if (infoPanel != null && infoPanel.activeSelf)
                    infoPanel.SetActive(false);
            }
            else if (dialoguePanel.activeSelf)
                dialoguePanel.SetActive(false);
        }
        else if (inkStory.currentChoices.Count > 0)
        {
            List<Choice> choices = inkStory.currentChoices;
            if (choices[0].text == "0")
            {
                HideDialogue();
                paused = true;
            }
            else
            {
                int choiceCount = choices.Count;
                float topPos = choiceDistance * (choiceCount - 1) / 2;
                Vector2 parentPos = choiceParent.position;
                for (int i = 0; i < choiceCount; i++)
                {
                    Transform choice = Instantiate(choicePrefab, parentPos + Vector2.up * (topPos - choiceDistance * i),
                        Quaternion.identity, choiceParent).transform;
                    choice.GetChild(0).GetComponent<TextMeshProUGUI>().text = choices[i].text;

                    ChoiceButton choiceData = choice.GetComponent<ChoiceButton>();
                    choiceData.dialogueDisplay = this;
                    choiceData.choiceIndex = i;
                }
                choosing = true;
            }
        }
        else
        {
            HideDialogue();

            gameManager.RegisterDialogue(inkScript, seeing);
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

        choosing = false;
        paused = false;
        for (int i = choiceParent.childCount - 1; i >= 0; i--)
            Destroy(choiceParent.GetChild(i).gameObject);
        inkStory.ChooseChoiceIndex(index);
        NextLine();
    }

    public void ChoosePathString(string path)
    {
        int pathIndex;
        if (int.TryParse(path.Substring(0, 1), out pathIndex))
            seeing = pathIndex;
        choosing = false;
        paused = false;
        for (int i = choiceParent.childCount - 1; i >= 0; i--)
            Destroy(choiceParent.GetChild(i).gameObject);
        inkStory.ChoosePathString(path);
        NextLine();
    }

    public void InfoSetup(GameObject infoPanelInstance)
    {
        infoPanel = infoPanelInstance;
        if (infoPanel == null)
            return;

        infoBox = infoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        infoImage = infoPanel.GetComponent<Image>();
        infoAlpha = infoImage.color.a;
        infoPanel.SetActive(false);
    }

    public void InfoText(string text)
    {
        if (infoPanel == null)
            return;

        infoPanel.SetActive(true);
        infoImage.color += new Color(0, 0, 0, infoAlpha - infoImage.color.a);
        infoBox.color += new Color(0, 0, 0, 1 - infoBox.color.a);
        infoBox.text = text;
        infoTimer = infoTime + infoFadeTime;
        infoTiming = true;
    }

    void DestroyPanels(Scene scene)
    {
        if (dialoguePanel != null)
            Destroy(dialoguePanel);
        if (infoPanel != null)
            Destroy(infoPanel);
        if (choiceParent != null)
            Destroy(choiceParent.gameObject);
    }

    public void LoadScene(string sceneName) => SceneManager.LoadScene(sceneName);
}
