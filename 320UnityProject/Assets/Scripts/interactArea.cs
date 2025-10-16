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
    private TMP_Text infoBox;
    private Image infoImage;
    private float infoAlpha;
    private float infoTimer = 0;

    public Player playerScript;
    private bool pickedUp;
    // Start is called before the first frame update

    private void Awake()
    {      
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += InfoSetup;
    }
    void Start()
    {
        InfoSetup();

        playerScript = FindAnyObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (infoPanel)
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
                infoPanel.SetActive(false);
                infoImage.color += new Color(0, 0, 0, infoAlpha - infoImage.color.a);
                infoBox.color += new Color(0, 0, 0, 1 - infoBox.color.a);
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
            //if you can pick it up add to inventory
            if(script.canPickup)
            {
                playerScript.AddToInventory(other.gameObject);
                
                other.gameObject.transform.position = new Vector3(100, 100, 100);
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
                        Debug.Log(script.endpointDialogue);
                        GameObject temp = playerScript.GetInventory()[i];
                        playerScript.GetInventory().RemoveAt(i);
                        Destroy(temp);
                        Destroy(other.gameObject);
                        pickedUp=true;
                    }
                }
            }
            //if dialogue send it to infoBox and debug
            if (script.isDialogue && !pickedUp)
            {
                if (infoPanel != null)
                {
                    if (!infoPanel.activeSelf)
                        infoPanel.SetActive(true);
                    infoBox.text = script.dialogue;
                    infoTimer = infoTime + infoFadeTime;
                }
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

    }

    private void InfoSetup(Scene scene, LoadSceneMode mode)
    {
        InfoSetup();
    }
    private void InfoSetup()
    {
        infoBox = GameObject.FindWithTag("UIController").GetComponent<UIController>().infoBox;
        if (!infoBox)
            return;

        infoPanel = infoBox.transform.parent.gameObject;
        infoImage = infoPanel.GetComponent<Image>();
        infoAlpha = infoImage.color.a;
        if (infoPanel.activeSelf)
            infoPanel.SetActive(false);
    }
}
