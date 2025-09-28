using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactArea : MonoBehaviour
{
    public IsoPlayerMovement playerScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<interactableObject>() != null)
        {
          
            interactableObject script = other.gameObject.GetComponent<interactableObject>();
            if(script.canPickup)
            {
                playerScript.inventory.Add(other.gameObject);
                
                other.gameObject.transform.position = new Vector3(100, 100, 100);
            }
            if(script.isEndpoint)
            {
               
                int idNeeded = script.id;
                for (int i = 0; i < playerScript.inventory.Count; i++)
                {
                   
                    interactableObject scriptTwo = playerScript.inventory[i].GetComponent<interactableObject>();
                    if (scriptTwo.id == idNeeded)
                    {
                        GameObject temp = playerScript.inventory[i];
                        playerScript.inventory.RemoveAt(i);
                        Destroy(temp);
                        Destroy(other.gameObject);
                    }
                }
            }
            if (script.isDialogue)
            {
                Debug.Log(script.dialogue);
            }


        }
    }
    
}
