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
