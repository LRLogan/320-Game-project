using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    private bool entered = false;
    [SerializeField] bool destroy;
    [SerializeField] GameObject objectDestroy;
    [SerializeField] UnityEvent onEnter;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "PuzzleObject1" && !entered)
        {
            entered = true;
            Debug.Log("solved push puzzle");
            if (destroy)
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
            if(objectDestroy != null)
            {
                Destroy(objectDestroy);
            }
            onEnter.Invoke();
        }
    }
}


