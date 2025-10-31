using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorUnlock : MonoBehaviour
{
    [SerializeField] SceneWarpTrigger script;
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
    }
}
