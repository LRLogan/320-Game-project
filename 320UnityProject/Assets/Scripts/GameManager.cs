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
}
