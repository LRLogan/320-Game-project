using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneWarpTrigger : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("Name of the scene to load when player interacts")]
    public string sceneToLoad;
    [SerializeField] private bool goingInside;

    private bool playerInRange = false;

    private GameManager gameManager;
    private Player player;
    private InventoryManager inventoryManager;
    [SerializeField] Vector3 positionToLoad;
    public bool locked = false;
    private void Start()
    {
        //gameManager = FindFirstObjectByType<GameManager>();   
        player = FindFirstObjectByType<Player>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();  
    }

    void Update()
    {
        // Check if player is in the trigger zone and presses E
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            LoadScene();
        }
    }

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            if(!locked)
            {
                if (sceneToLoad != "FrogVille")
                {
                    player.posInOverworldBeforeSceneChange = player.transform.position;
                    Debug.Log($"Setting prev pos: {player.posInOverworldBeforeSceneChange}");     
                }
                player.posBeforeSceneChange = positionToLoad;
                SceneManager.LoadScene(sceneToLoad);
            }
           
            
        }
        else
        {
            Debug.LogWarning("Scene name is not set on SceneWarpTrigger.");
        }
    }
    /*
    // Detect when player enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Press E to enter the house.");
        }
    }

    // Detect when player exits the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    */
}
