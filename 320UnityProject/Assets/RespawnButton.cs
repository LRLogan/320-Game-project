using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnButton : MonoBehaviour
{

    [SerializeField] private Vector3 position;
    GameObject theObject;
    [SerializeField] float delay;
    [SerializeField] GameObject puzzleObject;
    public bool initialized = false;
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        if(theObject != null)
        {
            Destroy(theObject);
            theObject = Instantiate(puzzleObject
          , position,
          Quaternion.identity);
        }
        else if(!initialized)
        {
            theObject = Instantiate(puzzleObject
          , position,
          Quaternion.identity);
            initialized = !initialized;
            
        }
 
       
    }
}
