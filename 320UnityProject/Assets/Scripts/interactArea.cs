using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class interactArea : MonoBehaviour
{
    /// <summary>
    /// The number of seconds infoPanel is visible until it starts fading out.
    /// </summary>
    private const float infoTime = 2;

    /// <summary>
    /// The number of seconds it takes for infoPanel to completely disappear once it starts fading out.
    /// </summary>
    private const float infoFadeTime = 1;

    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI infoBox;
    private Image infoImage;
    private float infoAlpha;
    private float infoTimer = 0;
    private bool infoTiming = false;

    public Player playerScript;
    private bool pickedUp;
    // Start is called before the first frame update

    private void Awake()
    {      
        DontDestroyOnLoad(gameObject);
        //SceneManager.sceneLoaded += InfoSetup;
    }
    void Start()
    {
        //InfoSetup();

        playerScript = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (infoTiming && infoPanel != null)
        {
            if (infoTimer > 0)
            {
                infoTimer = Mathf.Max(0, infoTimer - Time.deltaTime);
                if (infoTimer <= infoFadeTime)
                {
                    infoImage.color -= new Color(0, 0, 0, Time.deltaTime * infoAlpha / infoFadeTime);
                    infoBox.color -= new Color(0, 0, 0, Time.deltaTime / infoFadeTime);
                }
            }
            else if (infoPanel.activeSelf)
            {
                infoImage.color += new Color(0, 0, 0, infoAlpha - infoImage.color.a);
                infoBox.color += new Color(0, 0, 0, 1 - infoBox.color.a);
                infoPanel.SetActive(false);
                infoTiming = false;
            }
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        
        pickedUp = false;
        //if object is interactible get its script
        if (other.gameObject.GetComponent<interactableObject>() != null)
        {
         
            interactableObject script = other.gameObject.GetComponent<interactableObject>();
            script.wasInteracted = true;
            //if you can pick it up add to inventory
            if(script.canPickup)
            {
                playerScript.AddToInventory(other.gameObject);
                DontDestroyOnLoad(other.gameObject);
                if(!script.destroyOnPickup)
                {
                    other.gameObject.transform.position = new Vector3(100, 100, 100);
                }
                
            }
            //if endpoint find item in inventory and remove it
            if(script.isEndpoint)
            {
               
                int idNeeded = script.id;
                for (int i = 0; i < playerScript.GetInventory().Count; i++)
                {
                   
                    interactableObject scriptTwo = playerScript.GetInventory()[i].GetComponent<interactableObject>();
                    if (scriptTwo.id == idNeeded)
                    {
                        InfoText(script.endpointDialogue);
                        Debug.Log(script.endpointDialogue);
                        GameObject temp = playerScript.GetInventory()[i];
                        playerScript.GetInventory().RemoveAt(i);
                        Debug.Log($"Destroying {temp.name} in inventory at slot: " + i);
                        Destroy(temp);
                        Destroy(other.gameObject);
                        pickedUp=true;
                    }
                }
            }
            //if dialogue send it to infoBox and debug
            if (script.isDialogue && !pickedUp)
            {
                InfoText(script.dialogue);
                Debug.Log(script.dialogue);
            }
         

        }
        if (other.gameObject.GetComponent<SceneWarpTrigger>() != null)
        {
            
            other.gameObject.GetComponent<SceneWarpTrigger>().LoadScene();
        }
        if (other.gameObject.GetComponent<RespawnButton>() != null)
        {

            other.gameObject.GetComponent<RespawnButton>().Reset();
        }
        
        if (other.gameObject.GetComponent<MultiBlockPuzzle>() != null)
        {
           
            other.gameObject.GetComponent<MultiBlockPuzzle>().Interacted();
        }
        if (other.gameObject.GetComponent<findAllPuzzle>() != null)
        {

            other.gameObject.GetComponent<findAllPuzzle>().Interacted();
        }
        if (other.gameObject.GetComponent<DoorUnlock>() != null)
        {

            other.gameObject.GetComponent<DoorUnlock>().Interact();
        }

    }

    private void InfoSetup(Scene scene, LoadSceneMode mode)
    {
        InfoSetup(null);
    }
    public void InfoSetup(GameObject infoPanelInstance)
    {
        infoPanel = infoPanelInstance;
        if (infoPanel == null)
            return;

        infoBox = infoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        infoImage = infoPanel.GetComponent<Image>();
        infoAlpha = infoImage.color.a;
        infoPanel.SetActive(false);
    }

    private void InfoText(string text)
    {
        if (infoPanel == null)
            return;

        infoPanel.SetActive(true);
        infoBox.text = text;
        infoTimer = infoTime + infoFadeTime;
        infoTiming = true;
    }
}
