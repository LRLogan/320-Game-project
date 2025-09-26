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
            playerScript.inventory.Add(other.gameObject);
            Debug.Log(other.gameObject.name);
            other.gameObject.transform.position = new Vector3(100, 100, 100);
        }
    }
    
}
