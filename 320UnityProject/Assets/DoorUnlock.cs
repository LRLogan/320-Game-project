using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class DoorUnlock : MonoBehaviour
{
    [SerializeField] SceneWarpTrigger script;
    [SerializeField] GameObject doorObj;
    [SerializeField] GameObject doorObj2;
    [SerializeField] UnityEvent onUnlock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        Debug.Log("awdawd");
        if (script.locked)
            onUnlock.Invoke();
        script.locked = false;

        if(doorObj != null && doorObj2 != null)
        {
            doorObj.SetActive(false);
            doorObj2.SetActive(true);
        }
    }
}
