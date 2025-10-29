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
            Debug.Log(playerInstance.GetComponent<Player>().posBeforeSceneChange);
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
        Debug.Log(playerInstance.name);
        if (camera.GetComponent<sCameraInterior>() != null)
        {
            camera.GetComponent<sCameraInterior>().player = playerInstance.transform;
        }
        else
        {
            camera.GetComponent<sSimpleCamera>().player = playerInstance.transform;
        }
       

        interactArea.playerScript = playerInstance.GetComponent<Player>();
        inventoryUI.player = playerInstance.GetComponent<Player>();

    }

    /// <summary>
    /// Actions that need to happen at start time rather than awake
    /// </summary>
    private void Start()
    {
        inventoryUI.RefreshUI();
    }

}
