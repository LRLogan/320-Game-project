using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    private bool entered = false;
    [SerializeField] bool destroy;
    [SerializeField] GameObject objectDestroy;
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
        }
    }
}


