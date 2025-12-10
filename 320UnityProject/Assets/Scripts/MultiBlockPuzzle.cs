using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBlockPuzzle : MonoBehaviour
{
    [SerializeField] int blockNumber;
    public bool hasPressed;
    [SerializeField] public MultiBlockPuzzleManager manager;
    [SerializeField] public bool dialogue;
    [SerializeField] public string dialogueString;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interacted()
    {
        
            manager.updatePuzzle(blockNumber);
            Debug.Log("awdawd");
            
    }




}
