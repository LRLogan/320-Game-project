using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class interactableObject : MonoBehaviour
{

    public bool canPickup = true;
    public bool destroyOnPickup = false;
    public bool isEndpoint = false;
    public string endpointDialogue;
    public bool isDialogue = false;
    public string dialogue = "hi";
    public bool isEvent = false;
    public UnityEvent onInteract;
    public bool isEndpointEvent = false;
    public UnityEvent onEndpoint;
    public bool wasInteracted = false;
    public int id = 1;
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
