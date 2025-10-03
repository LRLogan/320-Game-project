using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data structure for Puzzle Tracker 
/// </summary>
public class Puzzle : ScriptableObject
{
    public bool isStarted = false;
    public bool isCompleted = false;
    public bool hasCompletionCondition = false;
    public string puzzleName;
    public Puzzle nextPuzzle;

    public void Init(string name)
    {
        puzzleName = name;
    }
}
