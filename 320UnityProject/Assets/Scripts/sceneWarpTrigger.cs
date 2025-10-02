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

    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();   
    }

    void Update()
    {
        // Check if player is in the trigger zone and presses E
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if(goingInside)
            {
                gameManager.EnterIndoorScene(sceneToLoad);
            }
            else
            {
                gameManager.EnterMainOutside(gameManager.player.posBeforeSceneChange);
            }
        }
    }

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
}
