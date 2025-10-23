using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTracker : MonoBehaviour
{
    public Dictionary<string, Puzzle> progressList;
    public string curPuzzle;

    // Start is called before the first frame update
    void Start()
    {
        InsertData();
    }

    private void InsertData()
    {
        string curName = "Buff Frogs letter";

        CreatePuzzleInList(curName);

        // For additional puzzles change the curName and call the CreatePuzzleInList method
    }

    private void CreatePuzzleInList(string curName)
    {
        progressList[curName] = new Puzzle();
        progressList[curName].Init(curName);
    }

    public bool ProgressToNext()
    {
        if (progressList[curPuzzle].nextPuzzle != null)
        {
            curPuzzle = progressList[curPuzzle].nextPuzzle.name;
            return true;
        }
        return false;
    }
}
