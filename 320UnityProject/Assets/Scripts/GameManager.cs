using JetBrains.Annotations;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // This instance
    private GameObject managerObjInstance;
    private GameManager instance;

    // Player
    public Player player;
    private static PlayerData playerData;

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
        playerData = this.gameObject.AddComponent<PlayerData>();
        
    }

    /// <summary>
    /// Option on the start screen to load game to a checkpoint
    /// </summary>
    public void LoadSave()
    {
        // Get the local JSON file 
        SceneManager.LoadScene(startingSceneName); // THIS LINE IS TEMP WHILE JSON LOGIC IS NOT DONE

        GetSaveData(Path.Combine(Application.streamingAssetsPath, "PlayerSaveData"));
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
                playerData = JsonUtility.FromJson<PlayerData>(data);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Setting up the post request
    /// </summary>
    /// <param name="url">where to send the data</param>
    /// <param name="jsonData">json data</param>
    /// <returns></returns>
    private async Task AsyncPostSetUp(string url, string json)
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            UnityWebRequestAsyncOperation operation = webRequest.SendWebRequest();
            while (!operation.isDone)
                await Task.Yield();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("POST ERROR: " + webRequest.error);
            }
            else
            {
                Debug.Log("POST Success");
            }
        }
    }

    /// <summary>
    /// Starts the corutine for sending json data
    /// </summary>
    /// <param name="thisPlayer">the player whos data will be set</param>
    /// <returns></returns>
    public async void PostSaveData(PlayerData thisPlayer)
    {
        Debug.Log("Called PostSaveData in GameManager");
        // Serializing data to send back
        string json = JsonConvert.SerializeObject(thisPlayer);
        Debug.Log("Json: " + json);

        await AsyncPostSetUp("PATH", json);
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
