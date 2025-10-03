using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // This instance
    private GameObject managerObjInstance;
    private GameManager instance;

    // Player
    public Player player;

    private PuzzleTracker puzzleTracker;
    public Puzzle curPuzzle;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            managerObjInstance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        puzzleTracker = GetComponent<PuzzleTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterIndoorScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            player.posBeforeSceneChange = player.transform.position;
            SceneManager.LoadScene(sceneName);
            player.isInside = true;
        }
        else
        {
            Debug.LogWarning("Scene name is not set on SceneWarpTrigger.");
        }
    }

    /// <summary>
    /// Enter the main outside world scene 
    /// </summary>
    /// <param name="placementPos">position to spawn the player using world cordinates</param>
    public void EnterMainOutside(Vector3 placementPos)
    {
        SceneManager.LoadScene("GrayBox");

        // set player location
        player.transform.position = placementPos;
    }

    /// <summary>
    /// This function will be called whenever a scene that can trigger a puzzle is changed to 
    /// Other puzzle activations like item pickups will be handled in a similar function
    /// </summary>
    /// <param name="sceneName"></param>
    private void PuzzleInitFromSceneCheck(string sceneName)
    {
        switch (sceneName)
        {
            // Buff frogs letter puzzle will trigger the first time player enters Dead's grey box scene
            case "Dead's Grey Box":
                Puzzle checkingPuzzleAt = puzzleTracker.progressList[puzzleTracker.curPuzzle];
                if (checkingPuzzleAt.puzzleName == "Buff Frogs letter" &&
                    !checkingPuzzleAt.isStarted)
                {
                    checkingPuzzleAt.isStarted = true;
                    curPuzzle = checkingPuzzleAt;
                }
                break;
        }
    }
}
