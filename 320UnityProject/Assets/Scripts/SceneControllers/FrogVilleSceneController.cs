using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FrogVilleSceneController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject playerSpawnPoint;
    private GameObject playerInstance = null;

    [SerializeField] private InventoryManager inventoryUI;
    [SerializeField] private Camera camera;
    [SerializeField] private interactArea interactArea;

    private void Awake()
    {
        try
        {
            playerInstance = FindAnyObjectByType<Player>().gameObject;
            playerInstance.transform.position = playerInstance.GetComponent<Player>().posBeforeSceneChange;
        }
        catch
        {
            if (playerInstance == null)
            {
                Debug.Log("Player creation called in scene controller");
                playerInstance = Instantiate(playerPrefab, playerSpawnPoint.transform.position, Quaternion.identity);
            }
        }
        
        playerInstance.GetComponent<Player>().inventoryUI = inventoryUI;
        camera.GetComponent<sSimpleCamera>().player = playerInstance.transform;
        interactArea.playerScript = playerInstance.GetComponent<Player>();
        inventoryUI.player = playerInstance.GetComponent<Player>();
    }

}
