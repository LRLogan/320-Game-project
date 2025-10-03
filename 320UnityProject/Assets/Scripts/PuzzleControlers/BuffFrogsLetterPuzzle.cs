using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scene manager for when the Buff Frogs letter puzzle is active
/// </summary>
public class BuffFrogsLetterPuzzle : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private Collider door;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        if(gameManager.curPuzzle.puzzleName == "Buff Frogs letter" &&
            gameManager.curPuzzle.isStarted)
        {

        }
    }

    
}
