using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tracks the puzzle progress the player has made
/// </summary>
public class PuzzleTracker : MonoBehaviour
{
    // Maps the puzzle data struct to an identifier
    public Dictionary<string, Puzzle> progressList;
    public string curPuzzle;

    private void Start()
    {
        InsertData();

        // Starting puzzle
        curPuzzle = "Buff frogs letter";
    }
    // Insert data into dict
    private void InsertData()
    {
        string curName = "Buff Frogs letter";

        progressList[curName] = new Puzzle();
        progressList[curName].Init(curName);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>True if puzzle was advanced</returns>
    public bool ProgressToNext()
    {
        if(progressList[curPuzzle].nextPuzzle != null)
        {
            curPuzzle = progressList[curPuzzle].nextPuzzle.name;
            return true;
        }
        return false;
    }
}
