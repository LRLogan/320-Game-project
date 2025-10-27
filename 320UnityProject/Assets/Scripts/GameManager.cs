using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    // This instance
    private GameObject managerObjInstance;
    private GameManager instance;

    // Player
    public Player player;

    [SerializeField] private string startingSceneName;

    // Puzzle / progress tracking
    private Puzzle curPuzzle;
    private PuzzleTracker puzzleTracker;    // Attach to this game object 

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

    /// <summary>
    /// Option on the starting screen to start a new game
    /// </summary>
    public void NewGame()
    {
        SceneManager.LoadScene(startingSceneName);

        // Create default information JSON file
    }

    /// <summary>
    /// Option on the start screen to load game to a checkpoint
    /// </summary>
    public void LoadSave()
    {
        // Get the local JSON file 
        SceneManager.LoadScene(startingSceneName); // THIS LINE IS TEMP WHILE JSON LOGIC IS NOT DONE
        // Assign vars like position from JSON
    }

    private async Task<bool> GetSaveData(string pathToFile)
    {
        string getPath = "";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(getPath))
        {
            UnityWebRequestAsyncOperation operation = webRequest.SendWebRequest();
            while (!operation.isDone)
                await Task.Yield();

            // Checking request
            if(webRequest.result == UnityWebRequest.Result.Success)
            {
                string data = webRequest.downloadHandler.text;
                return true;
            }
            else
            {
                return false;
            }
        }
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

#region Not used for scene transition atm

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
        SceneManager.LoadScene("FrogVille");

        // set player location
        player.transform.position = placementPos;
    }
    #endregion
}
