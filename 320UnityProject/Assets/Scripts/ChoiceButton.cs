using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceButton : MonoBehaviour
{
    public DialogueDisplay dialogueDisplay;
    public int choiceIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseChoiceIndex() => dialogueDisplay.ChooseChoiceIndex(choiceIndex);
}
