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
    [SerializeField] Player player;


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

    public void EnterDeadsGreyBox()
    {
        SceneManager.LoadScene("Dead's Grey Box");
        player.isInside = true;
    }

    /// <summary>
    /// Enter the main outside world scene 
    /// </summary>
    /// <param name="placementPos">position to spawn the player</param>
    public void EnterMainOutside(Vector3 placementPos)
    {
        SceneManager.LoadScene("GrayBox");

        // set player location
    }
}
