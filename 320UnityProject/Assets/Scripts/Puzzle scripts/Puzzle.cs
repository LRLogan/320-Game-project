using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public bool isStarted = false, 
        isCompleated = false, 
        hasCompleationCondition = false;

    public string puzzleName;
    public Puzzle nextPuzzle;   // Essentailly set up like a linked list

    public void Init(string name)
    {
        puzzleName = name;
    }
}
